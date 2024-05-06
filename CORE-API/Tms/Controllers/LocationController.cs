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
    public class LocationController : GenericEntityController<Location, LocationInputResource, LocationOutputResource>
    {

        public LocationController(IControllerHelper controllerHelper, IGenericEntityService<Location> entityService,
                                  IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
          : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Location")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<LocationOutputResource> Create(LocationInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Location")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<LocationOutputResource> Update(Guid Id, LocationInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Location")]
        public override LocationOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Locations")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<CoreListOutputResource<LocationOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one location")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<LocationOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpGet("Filter")]
        [SwaggerSummary("Filter Locations")]
        // [Authorize(Roles = "Administrator")] 
        public async Task<CoreListOutputResource<LocationOutputResource>> Filter(string byNodeCode, string byNodeName, string byAddress)
        {
            var where = PredicateBuilder.New<Location>();

            if (!byNodeCode.IsNullOrEmpty())
            {
                where = where.And(m => m.NodeCode.ToUpper().Contains(byNodeCode.ToUpper()));
            }

            if (!byNodeName.IsNullOrEmpty())
            {
                where = where.And(m => m.NodeName.ToUpper().Contains(byNodeName.ToUpper()));
            }

            if (!byAddress.IsNullOrEmpty())
            {
                where = where.And(m => m.Address.ToUpper().Contains(byAddress.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<LocationOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Location>, IList<LocationOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }
    }
}
