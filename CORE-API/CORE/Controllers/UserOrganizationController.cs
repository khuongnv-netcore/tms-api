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
    public class UserOrganizationController : GenericEntityController<UserOrganization, UserOrganizationInputResource, UserOrganizationOutputResource>
    {
        private readonly IGenericEntityService<Organization> _organizationEntityService;

        public UserOrganizationController(IGenericEntityService<Organization> organizationEntityService, IControllerHelper controllerHelper, IGenericEntityService<UserOrganization> entityService, IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper) 
            : base (controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
            _organizationEntityService = organizationEntityService;
        }

        [HttpPost]
        [SwaggerSummary("Create UserOrganization")]
        public override Task<UserOrganizationOutputResource> Create(UserOrganizationInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpGet]
        [SwaggerSummary("List UserOrganizations")]
        public override Task<CoreListOutputResource<UserOrganizationOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One UserOrganization")]
        public override UserOrganizationOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpPut]
        [SwaggerSummary("Update UserOrganization")]
        public override Task<UserOrganizationOutputResource> Update(Guid id, UserOrganizationInputResource resource)
        {
            return base.Update(id, resource);
        }

        [HttpDelete]
        [SwaggerSummary("Delete UserOrganization")]
        public override Task<UserOrganizationOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }


    }
}
