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

namespace CORE_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : GenericEntityController<UserRole, UserRoleInputResource, UserRoleOutputResource>
    {

        public UserRoleController(IControllerHelper controllerHelper, IGenericEntityService<UserRole> entityService, IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper) 
            : base (controllerHelper, entityService, coreConfigurationOptions, mapper)
        {

        }

        [HttpPost]
        [SwaggerSummary("Assign Role to User")]
        public override Task<UserRoleOutputResource> Create(UserRoleInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpDelete]
        [SwaggerSummary("Unassign Role to User")]
        public override Task<UserRoleOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpGet]
        [SwaggerSummary("List all User Roles")]
        public override Task<CoreListOutputResource<UserRoleOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read a User Role")]
        public override UserRoleOutputResource Read(Guid id)
        {
            return base.Read(id);
        }
    }
}
