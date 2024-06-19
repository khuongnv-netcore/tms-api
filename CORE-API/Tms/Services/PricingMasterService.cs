using System;
using System.Collections.Generic;
using System.Linq;
using CORE_API.CORE.Contexts;
using CORE_API.Tms.Models.Entities;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using CORE_API.Tms.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Stripe;
using Org.BouncyCastle.Asn1.X509;
using CORE_API.Tms.Models.Views;
using CORE_API.Tms.Models.Enums;
using AutoMapper;
using CORE_API.CORE.Services.Abstract;
using CORE_API.CORE.Models.Views;

namespace CORE_API.Tms.Services
{
    public class PricingMasterService : IPricingMasterService
    {
        private IGenericEntityService<PricingMaster> _pricingMasterEntityService;
        private IGenericEntityService<PricingMasterDetail> _pricingMasterDetailEntityService;
        private IMapper _mapper;

        public PricingMasterService(
                IGenericEntityService<PricingMaster> pricingMasterEntityService,
                IGenericEntityService<PricingMasterDetail> pricingMasterDetailEntityService,
                IMapper mapper
            ) {
            _pricingMasterEntityService = pricingMasterEntityService;
            _pricingMasterDetailEntityService = pricingMasterDetailEntityService;
            _mapper = mapper;
        }

        public async Task<PricingMasterOutputResource> updatePricingMaster(Guid id, PricingMasterInputResource resource)
        {
            var entity = _pricingMasterEntityService.FindById(id);

            var updatePricingMasterInput = _mapper.Map<PricingMasterInputResource, UpdatePricingMasterInputResource>(resource);
            
            entity = _mapper.Map<UpdatePricingMasterInputResource, PricingMaster>(updatePricingMasterInput, entity);

            // Update Pricing Master Detail
            var pricingMasterDetails = _mapper.Map<IEnumerable<PricingMasterDetailInputResource>, List<PricingMasterDetail>>(resource.PricingMasterDetails);

            var pricingMasterDetailsResponse = await _pricingMasterDetailEntityService.BulkInsertOrUpdate(pricingMasterDetails);


            // Delete Pricing Master Detail
            var deletedEntites = entity.PricingMasterDetails.ToList().FindAll(m => !pricingMasterDetails.Select(m => m.Id).Contains(m.Id));
            await _pricingMasterDetailEntityService.DeleteMany(deletedEntites);

            // Update Pricing Master
            var result = await _pricingMasterEntityService.UpdateAsync(entity);

            if (!result.Success)
            {
                //TODO Throw Error
            }

            var output = _mapper.Map<PricingMaster, PricingMasterOutputResource>(result.Entity);

            return output;
        }


    }
}
