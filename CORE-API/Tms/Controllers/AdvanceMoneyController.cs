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
using CORE_API.Tms.Models.Enums;
using Twilio.TwiML.Voice;
using CORE_API.Tms.Services.Abstract;

namespace CORE_API.Tms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceMoneyController : GenericEntityController<AdvanceMoney, AdvanceMoneyInputResource, AdvanceMoneyOutputResource>
    {
        private IGenericEntityService<BookingCharge> _bookingChargeEntityService;
        private IAdvanceMoneyService _advanceMoneyService;
        public AdvanceMoneyController(IControllerHelper controllerHelper, IGenericEntityService<AdvanceMoney> entityService,
                                      IGenericEntityService<BookingCharge> bookingChargeEntityService,
                                      IAdvanceMoneyService advanceMoneyService,
                                      IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
          : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
            _bookingChargeEntityService = bookingChargeEntityService;
            _advanceMoneyService = advanceMoneyService;
        }

        [HttpPost]
        [SwaggerSummary("Create AdvanceMoney")]
        // [Authorize(Roles = "Administrator")] 
        public async override Task<AdvanceMoneyOutputResource> Create(AdvanceMoneyInputResource resource)
        {
            var advanceMoneyOutputResource = base.Create(resource);

            if (resource.BookingId != null && resource.BookingId != Guid.Empty)
            {
                List<BookingCharge> bookingCharges = new List<BookingCharge>();
                foreach (var item in advanceMoneyOutputResource.Result.AdvanceMoneyDocuments)
                {
                    if (!item.Internal) {
                        var bookingCharge = new BookingCharge
                        {
                            AdvanceMoneyDocumentId = item.Id,
                            UnitPrice = item.Money,
                            Vol = 1,
                            Amount = item.Money,
                            BookingId = resource.BookingId ?? Guid.Empty
                        };
                        bookingCharges.Add(bookingCharge);
                    }
                    
                }
                await _bookingChargeEntityService.AddManyAsync(bookingCharges);
            }
            
            return advanceMoneyOutputResource.Result;
        }

        [HttpPut]
        [SwaggerSummary("Update AdvanceMoney")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<AdvanceMoneyOutputResource> Update(Guid Id, AdvanceMoneyInputResource resource)
        {
            return _advanceMoneyService.updateAdvanceMoney(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read OneAdvanceMoney")]
        public override AdvanceMoneyOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List AdvanceMoney")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<CoreListOutputResource<AdvanceMoneyOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one Pricing for Customer")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<AdvanceMoneyOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpGet("Filter")]
        [AllowAnonymous]
        [SwaggerSummary("Filter AdvanceMoney")]
        public async Task<CoreListOutputResource<AdvanceMoneyOutputResource>> Filter(DateTime? Start, DateTime? End,
            [FromQuery(Name = "bookingIds[]")] List<Guid> bookingIds,
            [FromQuery(Name = "employeeIds[]")] List<Guid> employeeIds,
            int skip = 0, int count = 20)
        {

            var where = PredicateBuilder.New<AdvanceMoney>(true);
            if (Start.HasValue && End.HasValue)
            {
                where = where.And(m => m.Created >= Start && m.Created <= End);
                where = where.Or(m => m.Modified >= Start && m.Modified <= End);
            }
            else if (Start.HasValue)
            {
                where = where.And(m => m.Created >= Start);
                where = where.Or(m => m.Modified >= Start);
            }

            if (bookingIds.Count > 0)
            {
                where = where.And(m => bookingIds.Contains(m.BookingId ?? Guid.Empty));
            }

            if (employeeIds.Count > 0)
            {
                where = where.And(m => employeeIds.Contains(m.EmployeeId ?? Guid.Empty));
            }

            var results = _entityService.FindQueryableList(skip, count, where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<AdvanceMoneyOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<AdvanceMoney>, IList<AdvanceMoneyOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }

        [HttpGet("getAdvanceMoneyForBooking")]
        [SwaggerSummary("getAdvanceMoneyForBooking")]
        // [Authorize(Roles = "Administrator")] 
        public async Task<CoreListOutputResource<AdvanceMoneyOutputResource>> getAdvanceMoneyForBooking(Guid bookingId)
        {
            var where = PredicateBuilder.New<AdvanceMoney>();
            where = where.And(m => m.BookingId == bookingId);

            var results = _entityService.FindQueryableList(-1, -1, where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<AdvanceMoneyOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<AdvanceMoney>, IList<AdvanceMoneyOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }
    }
}