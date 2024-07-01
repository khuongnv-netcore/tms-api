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
    public class KindOfFeeController : GenericEntityController<KindOfFee, KindOfFeeInputResource, KindOfFeeOutputResource>
    {
        public KindOfFeeController(IControllerHelper controllerHelper, IGenericEntityService<KindOfFee> entityService,
                                   IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
          : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Kind Of Fee")]
        [Authorize(Roles = "Administrator")]
        public override Task<KindOfFeeOutputResource> Create(KindOfFeeInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Kind Of Fee")]
        [Authorize(Roles = "Administrator")]
        public override Task<KindOfFeeOutputResource> Update(Guid Id, KindOfFeeInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Kind Of Fee")]
        public override KindOfFeeOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Kind Of Fees")]
        [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<KindOfFeeOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one Kind Of Fee")]
        [Authorize(Roles = "Administrator")]
        public override Task<KindOfFeeOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}