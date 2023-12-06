using AutoMapper;
using CORE_API.CORE.Contexts;
using CORE_API.CORE.Controllers;
using CORE_API.CORE.Helpers;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : GenericEntityController<Role, RoleInputResource, RoleOutputResource>
    {

        public RoleController(IControllerHelper controllerHelper, IGenericEntityService<Role> entityService, IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper) 
            : base (controllerHelper, entityService, coreConfigurationOptions, mapper)
        {

        }

        [HttpPost]
        [SwaggerSummary("Create Role")]
        public override Task<RoleOutputResource> Create(RoleInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpGet]
        [SwaggerSummary("List Roles")]
        public override Task<CoreListOutputResource<RoleOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Role")]
        public override RoleOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpPut]
        [SwaggerSummary("Update Role")]
        public override Task<RoleOutputResource> Update(Guid id, RoleInputResource resource)
        {
            return base.Update(id, resource);
        }

        [HttpDelete]
        [SwaggerSummary("Delete Role")]
        public override Task<RoleOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }


    }
}
