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
    public class PricingForCustomerService : IPricingForCustomerService
    {
        private IGenericEntityService<PricingForCustomer> _pricingForCustomerEntityService;
        private IGenericEntityService<PricingForCustomerDetail> _pricingForCustomerDetailEntityService;
        private IMapper _mapper;

        public PricingForCustomerService(
                IGenericEntityService<PricingForCustomer> pricingForCustomerEntityService,
                IGenericEntityService<PricingForCustomerDetail> pricingForCustomerDetailEntityService,
                IMapper mapper
            ) {
            _pricingForCustomerEntityService = pricingForCustomerEntityService;
            _pricingForCustomerDetailEntityService = pricingForCustomerDetailEntityService;
            _mapper = mapper;
        }

        public async Task<PricingForCustomerOutputResource> updatePricingForCustomer(Guid id, PricingForCustomerInputResource resource)
        {
            var entity = _pricingForCustomerEntityService.FindById(id);

            var updatePricingForCustomerInput = _mapper.Map<PricingForCustomerInputResource, UpdatePricingForCustomerInputResource>(resource);
            
            entity = _mapper.Map<UpdatePricingForCustomerInputResource, PricingForCustomer>(updatePricingForCustomerInput, entity);

            // Update Pricing ForCustomer Detail
            var pricingForCustomerDetails = _mapper.Map<IEnumerable<PricingForCustomerDetailInputResource>, List<PricingForCustomerDetail>>(resource.PricingForCustomerDetails);

            await _pricingForCustomerDetailEntityService.BulkInsertOrUpdate(pricingForCustomerDetails);


            // Delete Pricing ForCustomer Detail
            var deletedEntites = entity.PricingForCustomerDetails.ToList().FindAll(m => !pricingForCustomerDetails.Select(m => m.Id).Contains(m.Id));
            await _pricingForCustomerDetailEntityService.DeleteMany(deletedEntites);

            // Update Pricing ForCustomer
            var result = await _pricingForCustomerEntityService.UpdateAsync(entity);

            if (!result.Success)
            {
                //TODO Throw Error
            }

            var output = _mapper.Map<PricingForCustomer, PricingForCustomerOutputResource>(result.Entity);

            return output;
        }


    }
}
