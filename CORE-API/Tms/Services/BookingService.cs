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
        private IMapper _mapper;

        public BookingService(BookingContext bookingContext,
                IGenericEntityService<Schedule> scheduleService,
                IMapper mapper
            ) { 
            _bookingContext = bookingContext;
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        public async Task<List<BookingEx>> FilterBookingNo(string bookingNo) {
            var bookings = new List<BookingEx>();
            var where = PredicateBuilder.New<BookingEx>();
            if (!bookingNo.IsNullOrEmpty())
            {
                where = where.And(m => m.BookingNo.ToUpper().Contains(bookingNo.ToUpper()));
            }
            bookings = await _bookingContext.Set<BookingEx>().Where(where).ToListAsync();

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
    }
}
