using AutoMapper;
using CORE_API.CORE.Controllers;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Models.Configuration;
using CORE_API.Tms.Models.Views;
using CORE_API.Tms.Models.Entities;
using CORE_API.CORE.Services.Abstract;
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
using System.Data.Entity;
using CORE_API.Tms.Services.Abstract;
using CORE_API.Tms.Models.Enums;
using CORE_API.Tms.Services;

namespace CORE_API.Tms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : GenericEntityController<Schedule, ScheduleInputResource, ScheduleOutputResource>
    {
        private IGenericEntityService<BookingContainerDetail> _bookingContainerDetailService;
        private IBookingService _bookingService;

        public ScheduleController(IControllerHelper controllerHelper, IGenericEntityService<Schedule> entityService, 
            IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper,
                        IGenericEntityService<BookingContainerDetail> bookingContainerDetailService,
                        IBookingService bookingService
            )
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
            _bookingContainerDetailService = bookingContainerDetailService;
            _bookingService = bookingService;
        }

        [HttpPost]
        [SwaggerSummary("Create Schedule")]
        [Authorize(Roles = "Administrator")]
        public override Task<ScheduleOutputResource> Create(ScheduleInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPost("CreateOrUpdate")]
        [SwaggerSummary("Create Or Update Schedule")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateOrUpdate(List<CreateOrUpdateScheduleInputResource> resource)
        {
            var updateSchedules = _mapper.Map<IEnumerable<CreateOrUpdateScheduleInputResource>, List<Schedule>>(resource);

            await _entityService.BulkInsertOrUpdate(updateSchedules);

            //Update Container No to BookingContainerDetail

            foreach (var schedule in updateSchedules)
            {
                var bookingContainerDetail = _bookingContainerDetailService.FindById(schedule.BookingContainerDetailId);
                bookingContainerDetail.ContainerNo = schedule.ContainerNo;
                await _bookingContainerDetailService.UpdateAsync(bookingContainerDetail);
            }
            //Update Schedule Status for Booking
             await _bookingService.UpdateScheduleStatusForbooking(updateSchedules.Select(m => m.BookingId).FirstOrDefault());

            return Ok();
        }

        [HttpPut]
        [SwaggerSummary("Update Schedule")]
        [Authorize(Roles = "Administrator")]
        public override Task<ScheduleOutputResource> Update(Guid Id, ScheduleInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Schedule")]
        public override ScheduleOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Schedules")]
        [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<ScheduleOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpGet("Filter")]
        [AllowAnonymous]
        [SwaggerSummary("Filter Schedules")]
        public async Task<CoreListOutputResource<ScheduleOutputResource>> Filter(DateTime Start, DateTime End, 
            [FromQuery(Name = "bookingNos[]")] List<String> bookingNos,
            [FromQuery(Name = "drivers[]")] List<Guid> drivers,
            [FromQuery(Name = "containerTrucks[]")] List<Guid> containerTrucks,
            int skip = 0, int count = 20)
        {

            var where = PredicateBuilder.New<Schedule>();

            where = where.And(m => (m.PickupPlan > Start && m.PickupPlan < End)
                                   || (m.DeliveryPlan > Start && m.DeliveryPlan < End)
                            );

            if (bookingNos.Count > 0) {
                where = where.And(m => bookingNos.Contains(m.BookingNo));
            }
            if (drivers.Count > 0) {
                where = where.And(m => drivers.Contains(m.DriverId));
            }
            if (containerTrucks.Count > 0)
            {
                where = where.And(m => containerTrucks.Contains(m.ContainerTruckId));
            }

            var results = _entityService.FindQueryableList(skip, count, where).ToList();

            var entities = results.Select(m => new ScheduleOutputResource {
                Id = m.Id,
                ContainerNo = m.ContainerNo,
                BookingId = m.BookingId,
                ContainerCode = m.BookingContainerDetail.BookingContainer.ContainerCode,
                Created = m.Created,
                Modified = m.Modified,
                PickupPlan = m.PickupPlan,
                DeliveryPlan = m.DeliveryPlan,
                BookingNo = m.BookingNo,
                DriverId = m.DriverId,
                DriverName = m.DriverName,
                ContainerId = m.ContainerId,
                ScheduleStatus = m.ScheduleStatus,
                ContainerTruckCode = m.ContainerTruckCode,
                ContainerTruckId = m.ContainerTruckId,
                TransportCost = m.TransportCost,
                PickupAddress = m.PickupAddress,
                DeliveryAddress = m.DeliveryAddress
            });
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<ScheduleOutputResource>
            {
                Entities = entities,
                TotalEntities = total
            };

            return output;
        }

        [HttpDelete]
        [SwaggerSummary("Delete Schedule")]
        [Authorize(Roles = "Administrator")]
        public async override Task<ScheduleOutputResource> Delete(Guid id)
        {
            var schedule = _entityService.FindById(id);
            
            var output = await base.Delete(id);

            //Update Schedule Status for Booking
            await _bookingService.UpdateScheduleStatusForbooking(schedule.BookingId);

            return output;
        }
    }
}
