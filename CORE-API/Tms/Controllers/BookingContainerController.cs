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

namespace CORE_API.Tms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingContainerController : GenericEntityController<BookingContainer, BookingContainerInputResource, BookingContainerOutputResource>
    {
        public BookingContainerController(IControllerHelper controllerHelper, IGenericEntityService<BookingContainer> entityService, IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper)
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create Booking Container")]
        [Authorize(Roles = "Administrator")]
        public override Task<BookingContainerOutputResource> Create(BookingContainerInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update Booking Container")]
        [Authorize(Roles = "Administrator")]
        public override Task<BookingContainerOutputResource> Update(Guid Id, BookingContainerInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet]
        [SwaggerSummary("List Booking Container")]
        [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<BookingContainerOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }
    }
}
