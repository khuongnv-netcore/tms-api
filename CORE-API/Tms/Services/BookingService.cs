using System;
using System.Collections.Generic;
using System.Linq;
using CORE_API.CORE.Contexts;
using CORE_API.Tms.Models.Entities;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using CORE_API.Tms.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Stripe;
using Org.BouncyCastle.Asn1.X509;
using CORE_API.Tms.Models.Views;
using CORE_API.Tms.Models.Enums;
using AutoMapper;
using CORE_API.CORE.Services.Abstract;

namespace CORE_API.Tms.Services
{
    public class BookingService : IBookingService
    {
        private BookingContext _bookingContext;
        private IGenericEntityService<Schedule> _scheduleService;
        private IGenericEntityService<Booking> _bookingEntityService;
        private IGenericEntityService<BookingContainer> _bookingContainerEntityService;
        private IGenericEntityService<BookingContainerDetail> _bookingContainerDetailEntityService;
        private IGenericEntityService<BookingCharge> _bookingChargeEntityService;
        private IMapper _mapper;

        public BookingService(BookingContext bookingContext,
                IGenericEntityService<Schedule> scheduleService,
                IGenericEntityService<Booking> bookingEntityService,
                IGenericEntityService<BookingContainer> bookingContainerEntityService,
                IGenericEntityService<BookingContainerDetail> bookingContainerDetailEntityService,
                IGenericEntityService<BookingCharge> bookingChargeEntityService,
                IMapper mapper
            ) { 
            _bookingContext = bookingContext;
            _scheduleService = scheduleService;
            _bookingEntityService = bookingEntityService;
            _bookingContainerEntityService = bookingContainerEntityService;
            _bookingContainerDetailEntityService = bookingContainerDetailEntityService;
            _bookingChargeEntityService = bookingChargeEntityService;
            _mapper = mapper;
        }

        public async Task<List<Booking>> FilterBookingNo(string bookingNo) {
            var bookings = new List<Booking>();
            var where = PredicateBuilder.New<Booking>();
            if (!bookingNo.IsNullOrEmpty())
            {
                where = where.And(m => m.BookingNo.ToUpper().Contains(bookingNo.ToUpper()));
            }
            bookings = await _bookingContext.Set<Booking>().Where(where).ToListAsync();

            return bookings;
        }

        public async Task<List<BookingEx>> FilterEx(DateTime? Start, DateTime? End, [FromQuery(Name = "bookingNos[]")] List<string> bookingNos, int skip = 0, int count = 20)
        {
            var bookings = new List<BookingEx>();
            var where = PredicateBuilder.New<BookingEx>();
            Expression<Func<BookingEx, object>> defaultSort = (m => m.CreatedAt);

            if (Start.HasValue && End.HasValue)
            {
                where = where.And(m => m.CreatedAt >= Start && m.CreatedAt <= End);
            }
            else if (Start.HasValue)
            {
                where = where.And(m => m.CreatedAt >= Start);
            }

            if (bookingNos.Count > 0)
            {
                where = where.And(m => bookingNos.Contains(m.BookingNo));
            }

            bookings = await _bookingContext.Set<BookingEx>().Where(where).OrderBy(defaultSort).Skip(skip).Take(count).ToListAsync();

            return bookings;
        }

        public async Task<List<ScheduleForBookingOutputResource>> LoadScheduleForBookingsEx(List<string> bookingIds)
        {
            var scheduleForBookings = new List<ScheduleForBookingOutputResource>();
            var where = PredicateBuilder.New<BookingEx>();
            var bookingContainerWhere = PredicateBuilder.New<BookingContainerEx>();

            if (bookingIds.Count > 0)
            {
                where = where.And(m => bookingIds.Contains(m.Id));
                var bookings = await _bookingContext.Set<BookingEx>().Where(where).ToListAsync();

                // Get Booking Containers
                bookingContainerWhere = bookingContainerWhere.And(m => bookingIds.Contains(m.BookingId));
                var bookingContainers = await _bookingContext.Set<BookingContainerEx>().Where(bookingContainerWhere).ToListAsync();

                // Get Booking Container Details For Booking Containters
                var bookingContainerDetailWhere = PredicateBuilder.New<BookingContainerDetailEx>();
                bookingContainerDetailWhere = bookingContainerDetailWhere.And(
                                                m => bookingContainers.Select(bc => bc.Id)
                                                                      .Contains(m.BookingContainerId)
                                              );
                var bookingContainerDetails = await _bookingContext.Set<BookingContainerDetailEx>().Where(bookingContainerDetailWhere).ToListAsync();

                // Get Schedule for bookingContainerDetails
                var scheduleWhere = PredicateBuilder.New<Schedule>();
                scheduleWhere = scheduleWhere.And(
                    m => bookingContainerDetails.Select(bcd => bcd.Id)
                                                .Contains(m.BookingContainerDetailId.ToString().ToLower()));
                var schedules = await _scheduleService.FindAll(scheduleWhere).ToListAsync();

                //Update schedule for bookingContainerDetails

               bookingContainerDetails.ForEach(bookingContainerDetail =>
               {
                   bookingContainerDetail.Schedule = schedules.Where(m => m.BookingContainerDetailId.ToString() == bookingContainerDetail.Id).FirstOrDefault();
               });

               // Update BookingContainerDetail For bookingContainer
               bookingContainers.ForEach(bookingContainer =>
                {
                    bookingContainer.BookingContainerDetails = bookingContainerDetails.Where(m => m.BookingContainerId == bookingContainer.Id).ToList();
                });

                // Update BookingContainers of Booking
                bookings.ForEach(booking =>
                {
                    booking.BookingContainers = bookingContainers.Where(m => m.BookingId == booking.Id).ToList();

                    // Make schedule for bookings
                    var schedules = booking.BookingContainers
                                        .SelectMany(m => m.BookingContainerDetails)
                                        .Select(x => x.Schedule ?? new Schedule
                                        {
                                            BookingId = new Guid(x.BookingId),
                                            BookingNo = x.BookingNo,
                                            ContainerId = new Guid(x.ContainerId),
                                            BookingContainerId = new Guid(x.BookingContainerId),
                                            BookingContainer = new BookingContainer
                                            {
                                                ContainerCode = x.BookingContainer.ContainerCode
                                            },
                                            BookingContainerDetailId = new Guid(x.Id),
                                            ScheduleStatus = EScheduleStatus.ASSINGED,
                                            PickupPlan = DateTime.UtcNow,
                                            DeliveryPlan = DateTime.UtcNow,
                                            ContainerNo = x.ContainerNo,
                                            TransportCost = 0
                                        }).ToList();

                    var scheduleBookingOutputResource = _mapper.Map<IEnumerable<Schedule>, List<ScheduleBookingOutputResource>>(schedules);
                    var scheduleForBooking = new ScheduleForBookingOutputResource
                    {
                        BookingId = new Guid(booking.Id),
                        BookingNo = booking.BookingNo,
                        schedules = scheduleBookingOutputResource
                    };
                    scheduleForBookings.Add(scheduleForBooking);
                });
            }
            await _bookingContext.SaveChangesAsync();
            return scheduleForBookings;
        }

        public async Task UpdateScheduleStatusForbooking(Guid bookingId) {
            var booking = _bookingEntityService.FindById(bookingId);
            var bookingContainerDetails = booking.BookingContainers.SelectMany(m => m.BookingContainerDetails).ToList();
            var needScheduleCount = bookingContainerDetails.Count;
            var scheduledCount = bookingContainerDetails.Count(m => m.Schedule != null && m.Schedule.DeletedAt == null);
            var scheduleStatus = EScheduleStatusOfBooking.EMPTY;
            if (needScheduleCount == scheduledCount)
            {
                scheduleStatus = EScheduleStatusOfBooking.FULL;
            }
            else if (needScheduleCount > scheduledCount && scheduledCount > 0)
            {
                scheduleStatus = EScheduleStatusOfBooking.PARTIAL;
            }
            booking.ScheduleStatus = scheduleStatus;
            await _bookingEntityService.UpdateAsync(booking);
        }

        public async Task<BookingOutputResource> updateBooking(Guid id, BookingInputResource resource)
        {
            var entity = _bookingEntityService.FindById(id);

            var updateBookingInput = _mapper.Map<BookingInputResource, UpdateBookingInputResource>(resource);

            entity = _mapper.Map<UpdateBookingInputResource, Booking>(updateBookingInput, entity);

            // Update Booking Containers
            var bookingContainers = _mapper.Map<IEnumerable<BookingContainerInputResource>, List<BookingContainer>>(resource.BookingContainers);

            var bookingContainersResponse = await _bookingContainerEntityService.BulkInsertOrUpdate(bookingContainers);

            // Delete Booking Containers
            var deletedEntites = entity.BookingContainers.ToList().FindAll(m => !bookingContainers.Select(m => m.Id).Contains(m.Id));
            entity.BookingContainers = entity.BookingContainers.Where(item => !deletedEntites.Select(m => m.Id).Contains(item.Id)).ToList();
            await _bookingContainerEntityService.DeleteMany(deletedEntites);

            foreach (var bookingContainer in resource.BookingContainers) {
                // Update Booking Container Details
                var bookingContainerDetails = _mapper.Map<IEnumerable<BookingContainerDetailInputResource>, List<BookingContainerDetail>>(bookingContainer.bookingContainerDetails);
                var bookingContainerDetailsResponse = await _bookingContainerDetailEntityService.BulkInsertOrUpdate(bookingContainerDetails);
                // Delete Booking Container Details
                var deletedBookingContainerDetailEntites = entity.BookingContainers
                    . Where(m => m.Id == bookingContainer.Id)
                    .SelectMany(m => m.BookingContainerDetails)
                    .ToList().FindAll(m => !bookingContainerDetails.Select(m => m.Id).Contains(m.Id));

                await _bookingContainerDetailEntityService.DeleteMany(deletedBookingContainerDetailEntites);

            }

            // Update Booking Charges
            var bookingCharges = _mapper.Map<IEnumerable<BookingChargeInputResource>, List<BookingCharge>>(resource.BookingCharges);

            var bookingChargesResponse = await _bookingChargeEntityService.BulkInsertOrUpdate(bookingCharges);


            // Delete Booking Charges
            var deletedBookingChargeEntites = entity.BookingCharges.ToList().FindAll(m => !bookingCharges.Select(m => m.Id).Contains(m.Id));
            entity.BookingCharges = entity.BookingCharges.Where(item => !deletedBookingChargeEntites.Select(m => m.Id).Contains(item.Id)).ToList();
            await _bookingChargeEntityService.DeleteMany(deletedBookingChargeEntites);


            // Update Booking
            var result = await _bookingEntityService.UpdateAsync(entity);

            if (!result.Success)
            {
                //TODO Throw Error
            }

            var output = _mapper.Map<Booking, BookingOutputResource>(result.Entity);

            return output;
        }
    }
}
