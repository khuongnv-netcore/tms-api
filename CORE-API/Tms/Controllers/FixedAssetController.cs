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
    public class FixedAssetController : GenericEntityController<FixedAsset, FixedAssetInputResource, FixedAssetOutputResource>
    {

        public FixedAssetController(IControllerHelper controllerHelper, IGenericEntityService<FixedAsset> entityService
            , IOptions<CoreConfigurationOptions> coreConfigurationOptions, IMapper mapper
            )
            : base(controllerHelper, entityService, coreConfigurationOptions, mapper)
        {
        }

        [HttpPost]
        [SwaggerSummary("Create FixedAsset")]
        //[Authorize(Roles = "Administrator")]
        public override Task<FixedAssetOutputResource> Create(FixedAssetInputResource resource)
        {
            return base.Create(resource);
        }

        [HttpPut]
        [SwaggerSummary("Update FixedAsset")]
        // [Authorize(Roles = "Administrator")]
        public override Task<FixedAssetOutputResource> Update(Guid Id, FixedAssetInputResource resource)
        {
            return base.Update(Id, resource);
        }

        [HttpGet("{id}")]
        [SwaggerSummary("Read One FixedAsset")]
        public override FixedAssetOutputResource Read(Guid id)
        {
            return base.Read(id);
        }

        [HttpGet]
        [SwaggerSummary("List FixedAssets")]
        // [Authorize(Roles = "Administrator")]
        public override Task<CoreListOutputResource<FixedAssetOutputResource>> List(int skip = 0, int count = 20)
        {
            return base.List(skip, count);
        }

        [HttpGet("FilterContainerTruckCode")]
        [AllowAnonymous]
        [SwaggerSummary("Filter Container Truck Code")]
        public async Task<CoreListOutputResource<FixedAssetOutputResource>> FilterContainerTruckCode(string containerTruckCode)
        {

            var where = PredicateBuilder.New<FixedAsset>();
            where = where.And(m => m.FixedAssetType == EFixedAssetType.TRUCK);

            if (!containerTruckCode.IsNullOrEmpty())
            {
                where = where.And(m => m.FixedAssetCode.ToUpper().Contains(containerTruckCode.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<FixedAssetOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<FixedAsset>, IList<FixedAssetOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }

        [HttpDelete]
        [SwaggerSummary("Delete one fixed asset")]
        // [Authorize(Roles = "Administrator")] 
        public override Task<FixedAssetOutputResource> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpGet("Filter")]
        [SwaggerSummary("Filter Fixed Assets")]
        // [Authorize(Roles = "Administrator")] 
        public async Task<CoreListOutputResource<FixedAssetOutputResource>> Filter(string byFixedAssetCode, string byDriverName)
        {
            var where = PredicateBuilder.New<FixedAsset>();

            if (!byFixedAssetCode.IsNullOrEmpty())
            {
                where = where.And(m => m.FixedAssetCode.ToUpper().Contains(byFixedAssetCode.ToUpper()));
            }

            if (!byDriverName.IsNullOrEmpty())
            {
                where = where.And(m => m.DriverName.ToUpper().Contains(byDriverName.ToUpper()));
            }

            var results = _entityService.FindAll(where).ToList();
            var total = await _entityService.Count(where);

            var output = new CoreListOutputResource<FixedAssetOutputResource>
            {
                Entities = _mapper.Map<IEnumerable<FixedAsset>, IList<FixedAssetOutputResource>>(results),
                TotalEntities = total
            };

            return output;
        }
    }
}
