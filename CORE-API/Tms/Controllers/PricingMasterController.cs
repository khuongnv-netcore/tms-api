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
    public class PricingMasterController : GenericEntityController<PricingMaster, PricingMasterInputResource, PricingMasterOutputResource>
    {

        public PricingMasterController(IControllerHelper controllerHelper, IGenericEntityService<PricingMaster> entityService,
                                      IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
          : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Pricing Master")]
        // [Authorize(Roles = "Administrator")]
        public override Task<PricingMasterOutputResource> Create(PricingMasterInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Pricing Master")]
        // [Authorize(Roles = "Administrator")]
        public override Task<PricingMasterOutputResource> Update(Guid Id, PricingMasterInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Pricing Master")]
        public override PricingMasterOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Pricing Masters")]
        // [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<PricingMasterOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one Pricing Master")]
        // [Authorize(Roles = "Administrator")]
        public override Task<PricingMasterOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}