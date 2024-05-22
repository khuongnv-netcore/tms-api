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

namespace CORE_API.Tms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingForCustomerController : GenericEntityController<PricingForCustomer, PricingForCustomerInputResource, PricingForCustomerOutputResource>
    {

        public PricingForCustomerController(IControllerHelper controllerHelper, IGenericEntityService<PricingForCustomer> entityService,
                                      IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
          : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Pricing for Customer")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<PricingForCustomerOutputResource> Create(PricingForCustomerInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Pricing for Customer")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<PricingForCustomerOutputResource> Update(Guid Id, PricingForCustomerInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Pricing for Customer")]
        public override PricingForCustomerOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Pricing for Customers")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<CoreListOutputResource<PricingForCustomerOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one Pricing for Customer")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<PricingForCustomerOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpGet("Filter")]
        [SwaggerSummary("Filter Pricing For Customer")]
        // [Authorize(Roles = "Administrator")] 
        public async Task<CoreListOutputResource<PricingForCustomerOutputResource>> Filter(DateTime? byFromDate, DateTime? byToDate, Guid? byCustomer, Guid? bySaler)
        {
            var where = PredicateBuilder.New<PricingForCustomer>();
            if (byFromDate.HasValue && byToDate.HasValue)
            {
                where = where.And(p => p.FromDatePeriod >= byFromDate && p.ToDatePeriod <= byToDate);
            }
            else if (byFromDate.HasValue)
            {
                where = where.And(p => p.FromDatePeriod >= byFromDate);
            }
            else if (byToDate.HasValue)
            {
                where = where.And(p => p.ToDatePeriod <= byToDate);
            }

            if (byCustomer != Guid.Empty && byCustomer != null)
            {
                where = where.And(m => m.CustomerId== byCustomer);
            }

            if (bySaler != Guid.Empty && bySaler != null)
            {
                where = where.And(m => m.SellerId== bySaler);
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<PricingForCustomerOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<PricingForCustomer>, IList<PricingForCustomerOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }

        [HttpGet("getPricingForCustomerPeriod")]
        [SwaggerSummary("getPricingForCustomerPeriod")]
        // [Authorize(Roles = "Administrator")] 
        public PricingForCustomerOutputResource getPricingForCustomerPeriod(DateTime? currentDate, Guid customerId)
        {
            var where = PredicateBuilder.New<PricingForCustomer>();
            if (currentDate.HasValue)
            {
                where = where.And(p => currentDate >= p.FromDatePeriod && currentDate <= p.ToDatePeriod);
                where = where.And(m => m.CustomerId == customerId);
            }

            var result = _entityService.FindAll(where).ToList().FirstOrDefault();

            var output = _mapper.Map<PricingForCustomer, PricingForCustomerOutputResource>(result);

            return output;
        }
    }
}