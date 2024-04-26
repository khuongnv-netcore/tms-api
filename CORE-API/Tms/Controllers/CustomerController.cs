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
using Customer = CORE_API.Tms.Models.Entities.Customer;

namespace CORE_API.Tms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : GenericEntityController<Customer, CustomerInputResource, CustomerOutputResource>
    {

        public CustomerController(IControllerHelper controllerHelper, IGenericEntityService<Customer> entityService
            , IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper
            )
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Customer")]
        [Authorize(Roles = "Administrator")]
        public override Task<CustomerOutputResource> Create(CustomerInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Customer")]
        [Authorize(Roles = "Administrator")]
        public override Task<CustomerOutputResource> Update(Guid Id, CustomerInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        //[SwaggerSummary("Read One Customer")]
        public override CustomerOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Customers")]
        [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<CustomerOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one customer")]
        [Authorize(Roles = "Administrator")]
        public override Task<CustomerOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpGet("Filter")]
        [AllowAnonymous]
        [SwaggerSummary("Filter")]
        public async Task<CoreListOutputResource<CustomerOutputResource>> Filter(string byCustomerCode,string byLocationCode, string byLegalName, string byLanguageName, string byTaxCode)
        {

            var where = PredicateBuilder.New<Customer>();

            if (!byCustomerCode.IsNullOrEmpty())
            {
                where = where.And(m => m.Id.ToString().ToUpper().Contains(byCustomerCode.ToUpper()));
            }

            if (!byLocationCode.IsNullOrEmpty())
            {
                where = where.And(m => m.LocationCode.ToUpper().Contains(byLocationCode.ToUpper()));
            }

            if (!byLegalName.IsNullOrEmpty())
            {
                where = where.And(m => m.LegalName.ToUpper().Contains(byLegalName.ToUpper()));
            }

            if (!byLanguageName.IsNullOrEmpty())
            {
                where = where.And(m => m.LanguageName.ToUpper().Contains(byLanguageName.ToUpper()));
            }

            if (!byTaxCode.IsNullOrEmpty())
            {
                // Filter by tax code (case-insensitive substring match)
                where = where.And(m => m.TaxCode.ToUpper().Contains(byTaxCode.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<CustomerOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Customer>, IList<CustomerOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }
    }
}
