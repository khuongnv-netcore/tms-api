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
    public class EmployeeController : GenericEntityController<Employee, EmployeeInputResource, EmployeeOutputResource>
    {

        public EmployeeController(IControllerHelper controllerHelper, IGenericEntityService<Employee> entityService
            , IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Employee")]
        //[Authorize(Roles = "Administrator")]
        public override Task<EmployeeOutputResource> Create(EmployeeInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Employee")]
        //[Authorize(Roles = "Administrator")]
        public override Task<EmployeeOutputResource> Update(Guid Id, EmployeeInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One Employee")]
        //[Authorize(Roles = "Administrator")]
        public override EmployeeOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List Employees")]
        //[Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<EmployeeOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpDelete]
        [SwaggerSummary("Delete one employee")]
        //[Authorize(Roles = "Administrator")]
        public override Task<EmployeeOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        
        [HttpGet("Filter")]
        [SwaggerSummary("Filter Employees")]
        //[Authorize(Roles = "Administrator")]
        public async Task<CoreListOutputResource<EmployeeOutputResource>> Filter(
            string byEmployeeCode,
            string byEmployeeName,
            DateTime? byBirthday,
            string byCardNo)
        {
            var where = PredicateBuilder.New<Employee>(); // Use Employee here

            if (!byEmployeeCode.IsNullOrEmpty())
            {
                where = where.And(m => m.EmployeeCode.ToString().ToUpper().Contains(byEmployeeCode.ToUpper()));
            }

            if (!byEmployeeName.IsNullOrEmpty())
            {
                where = where.And(m => m.EmployeeName.ToUpper().Contains(byEmployeeName.ToUpper()));
            }

            if (byBirthday.HasValue)
            {
                where = where.And(m => m.Birthday.Date == byBirthday.Value.Date);
            }

            if (!byCardNo.IsNullOrEmpty())
            {
                where = where.And(m => m.CardNo.ToUpper().Contains(byCardNo.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList(); // Use Employee here
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<EmployeeOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<Employee>, IList<EmployeeOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }


    }
}