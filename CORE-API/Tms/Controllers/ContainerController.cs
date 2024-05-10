using AutoMapper;
using CORE_API.CORE.Controllers;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Models.Configuration;
using CORE_API.Tms.Models.Views; // Assuming view models are here
using CORE_API.Tms.Models.Entities; // Assuming entities are here
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
    public class ContainerController : GenericEntityController<Container, ContainerInputResource, ContainerOutputResource>
    {

        public ContainerController(IControllerHelper controllerHelper, IGenericEntityService<Container> entityService,
                                  IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
          : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Container")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<ContainerOutputResource> Create(ContainerInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Container")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<ContainerOutputResource> Update(Guid Id, ContainerInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Container")]
        public override ContainerOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Containers")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<CoreListOutputResource<ContainerOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one container")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<ContainerOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpGet("Filter")]
        [AllowAnonymous]
        [SwaggerSummary("Filter Containers")]
        public async Task<CoreListOutputResource<ContainerOutputResource>> Filter(string byContainerCode, string byContainerSize)
        {
            var where = PredicateBuilder.New<Container>();

            if (!byContainerCode.IsNullOrEmpty())
            {
                where = where.And(m => m.ContainerCode.ToUpper().Contains(byContainerCode.ToUpper()));
            }

            if (!byContainerSize.IsNullOrEmpty())
            {
                where = where.And(m => m.ContainerSize.ToUpper().Contains(byContainerSize.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<ContainerOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Container>, IList<ContainerOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }
    }
}
