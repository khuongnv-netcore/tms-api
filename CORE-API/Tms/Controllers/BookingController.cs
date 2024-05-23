using AutoMapper;
using CORE_API.CORE.Controllers;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Models.Configuration;
using CORE_API.Tms.Models.Views;
using CORE_API.Tms.Models.Entities;
using CORE_API.CORE.Services.Abstract;
using CORE_API.Tms.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using CORE_API.CORE.Models.Entities;
using Stripe;
using CORE_API.CORE.Services;
using Microsoft.Extensions.Options;
using CORE_API.CORE.Exceptions;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Views;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using CORE_API.Tms.Models.Enums;

namespace CORE_API.Tms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : GenericEntityController<Booking, BookingInputResource, BookingOutputResource>
    {
        private IGenericEntityService<BookingContainer> _bookingContainerService;
        private IGenericEntityService<BookingContainerDetail> _bookingContainerDetailService;
        private IGenericEntityService<Schedule> _scheduleService;
        private IBookingService _bookingService;
        public BookingController(IControllerHelper controllerHelper, IGenericEntityService<Booking> entityService
            , IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper,
            IGenericEntityService<BookingContainer> bookingContainerService,
            IGenericEntityService<BookingContainerDetail> bookingContainerDetailService,
            IGenericEntityService<Schedule> scheduleService,
            IBookingService bookingService
            )
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
            _bookingContainerService = bookingContainerService;
            _bookingContainerDetailService = bookingContainerDetailService;
            _scheduleService = scheduleService;
            _bookingService = bookingService;
        }

        [HttpPost]
        [SwaggerSummary("Create Booking")]
        [Authorize(Roles = "Administrator")]
        public override Task<BookingOutputResource> Create(BookingInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Booking")]
        [Authorize(Roles = "Administrator")]
        public override Task<BookingOutputResource> Update(Guid Id, BookingInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Booking")]
        public override BookingOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Bookings")]
        [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<BookingOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpGet("Filter")]
        [AllowAnonymous]
        [SwaggerSummary("Filter Bookings")]
        public async Task<CoreListOutputResource<BookingOutputResource>> Filter(DateTime? Start, DateTime? End, 
            [FromQuery(Name = "bookingNos[]")] List<string> bookingNos,
            [FromQuery(Name = "bookingStatuses[]")] List<EBookingStatus> bookingStatuses,
            [FromQuery(Name = "scheduleStatuses[]")] List<EScheduleStatusOfBooking> scheduleStatuses,
            int skip = 0, int count = 20)
        {

            var where = PredicateBuilder.New<Booking>(true);
            if (Start.HasValue && End.HasValue)
            {
                where = where.And(m => m.Created >= Start && m.Created <= End);
                where = where.Or(m => m.Modified >= Start && m.Modified <= End);
            }
            else if (Start.HasValue) {
                where = where.And(m => m.Created >= Start);
                where = where.Or(m => m.Modified >= Start);
            }

            if (bookingNos.Count > 0) {
                where = where.And(m => bookingNos.Contains(m.BookingNo));
            }

            if (bookingStatuses.Count > 0)
            {
                where = where.And(m => bookingStatuses.Contains(m.Status));
            }

            if (scheduleStatuses.Count > 0)
            {
                where = where.And(m => scheduleStatuses.Contains(m.ScheduleStatus));
            }

            var results = _entityService.FindQueryableList(skip, count, where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<BookingOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Booking>, IList<BookingOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }

        [HttpGet("FilterEx")]
        [AllowAnonymous]
        [SwaggerSummary("Filter Bookings Ex")]
        public async Task<CoreListOutputResourceEx<BookingOutputResourceEx>> FilterEx(DateTime? Start, DateTime? End, [FromQuery(Name = "bookingNos[]")] List<string> bookingNos, int skip = 0, int count = 20)
        {
            var results = await _bookingService.FilterEx(Start, End,  bookingNos, skip, count);
            var total = results.Count;

            var output = new CoreListOutputResourceEx<BookingOutputResourceEx>
            {
                Entities = _mapper.Map<IEnumerable<BookingEx>, IList<BookingOutputResourceEx>>(results),
                TotalEntities = total
            };

            return output;
        }

        [HttpGet("FilterBookingNo")]
        [AllowAnonymous]
        [SwaggerSummary("Filter BookingNo")]
        public async Task<CoreListOutputResource<BookingOutputResource>> FilterBookingNo(string bookingNo)
        {
            // FilterBookingNo from Internal Database
            #region
            var where = PredicateBuilder.New<Booking>();

            if (!bookingNo.IsNullOrEmpty())
            {
                where = where.And(m => m.BookingNo.ToUpper().Contains(bookingNo.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<BookingOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Booking>, IList<BookingOutputResource>>(results),
                TotalEntities = total
            };
            #endregion

            return output;
        }

        [HttpGet("FilterBookingNoEx")]
        [AllowAnonymous]
        [SwaggerSummary("Filter BookingNo Ex")]
        public async Task<CoreListOutputResourceEx<BookingOutputResourceEx>> FilterBookingNoEx(string bookingNo)
        {
            // FilterBookingNo from External Database
            #region
            var results = await _bookingService.FilterBookingNo(bookingNo);
            var total = results.Count;
            var output = new CoreListOutputResourceEx<BookingOutputResourceEx>
            {
                Entities = _mapper.Map<IEnumerable<BookingEx>, IList<BookingOutputResourceEx>>(results),
                TotalEntities = total
            };
            #endregion

            return output;
        }

        [HttpGet("LoadScheduleForBookings")]
        [AllowAnonymous]
        [SwaggerSummary("Load Schedule For Bookings")]
        public async Task<CoreScheduleListOutputResource<ScheduleForBookingOutputResource>> LoadScheduleForBookings([FromQuery(Name = "bookingIds[]")]List<Guid> bookingIds)
        {
            var scheduleForBookings = new List<ScheduleForBookingOutputResource>();
            var output = new CoreScheduleListOutputResource<ScheduleForBookingOutputResource> {
                Entities = scheduleForBookings,
                TotalEntities = 0
            };

            var where = PredicateBuilder.New<Booking>();

            if (bookingIds.Count > 0)
            {
                where = where.And(m => bookingIds.Contains(m.Id));

                var bookings = _entityService.FindAll(where).ToList();

                // Get Booking Containers
                var bookingContainerWhere = PredicateBuilder.New<BookingContainer>();
                bookingContainerWhere = bookingContainerWhere.And(m => bookingIds.Contains(m.BookingId));
                var bookingContainers = _bookingContainerService.FindAll(bookingContainerWhere).ToList();

                // Get Booking Container Details For Booking Containters
                var bookingContainerDetailWhere = PredicateBuilder.New<BookingContainerDetail>();
                bookingContainerDetailWhere = bookingContainerDetailWhere.And(
                                                m => bookingContainers.Select(bc => bc.Id)
                                                                      .Contains(m.BookingContainerId)
                                              );
                var bookingContainerDetails = _bookingContainerDetailService.FindAll(bookingContainerDetailWhere).ToList();

                // Get Schedule for bookingContainerDetails
                var scheduleWhere = PredicateBuilder.New<Schedule>();
                scheduleWhere = scheduleWhere.And(
                    m => bookingContainerDetails.Select(bcd => bcd.Id)
                                                .Contains(m.BookingContainerDetailId));
                var schedules = _scheduleService.FindAll(scheduleWhere).ToList();

                // Update schedule for bookingContainerDetails
                bookingContainerDetails.ForEach(bookingContainerDetail =>
                {
                    bookingContainerDetail.Schedule = schedules.Where(m => m.BookingContainerDetailId == bookingContainerDetail.Id).FirstOrDefault();
                });

                // Update BookingContainerDetail For bookingContainer
                bookingContainers.ForEach(bookingContainer =>
                {
                    bookingContainer.BookingContainerDetails = bookingContainerDetails.Where(m => m.BookingContainerId == bookingContainer.Id).ToList();
                });

                // Update BookingContainers of Booking
                bookings.ForEach(booking => {
                    booking.BookingContainers = bookingContainers.Where(m => m.BookingId == booking.Id).ToList();

                    // Make schedule for bookings
                    var schedules = booking.BookingContainers
                                        .SelectMany(m => m.BookingContainerDetails)
                                        .Select(x => x.Schedule ?? new Schedule {
                                            BookingId = x.BookingId,
                                            BookingNo = x.BookingNo,
                                            ContainerId = x.ContainerId,
                                            BookingContainerId = x.BookingContainerId,
                                            BookingContainer = new BookingContainer {
                                                ContainerCode = x.BookingContainer.ContainerCode
                                            },
                                            BookingContainerDetailId = x.Id,
                                            ScheduleStatus = EScheduleStatus.ASSINGED,
                                            PickupPlan = DateTime.UtcNow,
                                            DeliveryPlan = DateTime.UtcNow
                                        }).ToList();

                    var scheduleBookingOutputResource = _mapper.Map<IEnumerable<Schedule>, List<ScheduleBookingOutputResource>>(schedules);
                    var scheduleForBooking = new ScheduleForBookingOutputResource
                    {
                        BookingId = booking.Id,
                        BookingNo = booking.BookingNo,
                        schedules = scheduleBookingOutputResource
                    };
                    scheduleForBookings.Add(scheduleForBooking);
                });

                output.Entities = scheduleForBookings;
                output.TotalEntities = scheduleForBookings.Count;
            }

            return await Task.FromResult(output);
        }

        [HttpGet("LoadScheduleForBookingsEx")]
        [AllowAnonymous]
        [SwaggerSummary("Load Schedule For Bookings Ex")]
        public async Task<CoreScheduleListOutputResource<ScheduleForBookingOutputResource>> LoadScheduleForBookingsEx([FromQuery(Name = "bookingIds[]")] List<string> bookingIds)
        {
            var scheduleForBookings = new List<ScheduleForBookingOutputResource>();
            var output = new CoreScheduleListOutputResource<ScheduleForBookingOutputResource>
            {
                Entities = scheduleForBookings,
                TotalEntities = 0
            };

            scheduleForBookings = await _bookingService.LoadScheduleForBookingsEx(bookingIds);

            output.Entities = scheduleForBookings;
            output.TotalEntities = scheduleForBookings.Count;

            return await Task.FromResult(output);
        }
    }
}
