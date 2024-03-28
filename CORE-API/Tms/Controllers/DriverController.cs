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
    public class DriverController : GenericEntityController<Driver, DriverInputResource, DriverOutputResource>
    {

        public DriverController(IControllerHelper controllerHelper, IGenericEntityService<Driver> entityService
            , IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper
            )
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Drvier")]
        [Authorize(Roles = "Administrator")]
        public override Task<DriverOutputResource> Create(DriverInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Driver")]
        [Authorize(Roles = "Administrator")]
        public override Task<DriverOutputResource> Update(Guid Id, DriverInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Drvier")]
        public override DriverOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Drivers")]
        [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<DriverOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpGet("FilterDriverName")]
        [AllowAnonymous]
        [SwaggerSummary("Filter Driver Name")]
        public async Task<CoreListOutputResource<DriverOutputResource>> FilterDriverName(string driverName)
        {

            var where = PredicateBuilder.New<Driver>();

            if (!driverName.IsNullOrEmpty())
            {
                where = where.And(m => m.Name.ToUpper().Contains(driverName.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<DriverOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Driver>, IList<DriverOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }
    }
}
