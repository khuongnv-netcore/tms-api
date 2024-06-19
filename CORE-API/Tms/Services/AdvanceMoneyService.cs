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
    public class AdvanceMoneyService : IAdvanceMoneyService
    {
        private IGenericEntityService<AdvanceMoney> _advanceMoneyEntityService;
        private IGenericEntityService<AdvanceMoneyDocument> _advanceMoneyDocumentEntityService;
        private IMapper _mapper;

        public AdvanceMoneyService(
                IGenericEntityService<AdvanceMoney> advanceMoneyEntityService,
                IGenericEntityService<AdvanceMoneyDocument> advanceMoneyDocumentEntityService,
                IMapper mapper
            ) {
            _advanceMoneyEntityService = advanceMoneyEntityService;
            _advanceMoneyDocumentEntityService = advanceMoneyDocumentEntityService;
            _mapper = mapper;
        }

        public async Task<AdvanceMoneyOutputResource> updateAdvanceMoney(Guid id, AdvanceMoneyInputResource resource)
        {
            var entity = _advanceMoneyEntityService.FindById(id);

            var updateAdvanceMoneyInput = _mapper.Map<AdvanceMoneyInputResource, UpdateAdvanceMoneyInputResource>(resource);
            
            entity = _mapper.Map<UpdateAdvanceMoneyInputResource, AdvanceMoney>(updateAdvanceMoneyInput, entity);

            // Update Pricing Master Detail
            var advanceMoneyDocuments = _mapper.Map<IEnumerable<AdvanceMoneyDocumentInputResource>, List<AdvanceMoneyDocument>>(resource.AdvanceMoneyDocuments);

            var advanceMoneyDocumentsResponse =  await _advanceMoneyDocumentEntityService.BulkInsertOrUpdate(advanceMoneyDocuments);


            // Delete Pricing Master Detail
            var deletedEntites = entity.AdvanceMoneyDocuments.ToList().FindAll(m => !advanceMoneyDocuments.Select(m => m.Id).Contains(m.Id));
            await _advanceMoneyDocumentEntityService.DeleteMany(deletedEntites);

            // Update Booking Charges
            foreach (var item in entity.AdvanceMoneyDocuments)
            {
                if (item.AdvanceMoney.BookingId != null)
                {
                    if (item.BookingCharge != null)
                    {
                        // Update value
                        item.BookingCharge.UnitPrice = item.Money;
                        item.BookingCharge.Amount = item.Money;
                    }
                    else
                    {
                        // Add new booking charge
                        item.BookingCharge = new BookingCharge
                        {
                            BookingId = item.AdvanceMoney.BookingId ?? Guid.Empty,
                            AdvanceMoneyDocumentId = item.Id,
                            UnitPrice = item.Money,
                            Vol = 1,
                            Amount = item.Money
                        };
                    }
                }
            }

            // Update Pricing Master
            var result = await _advanceMoneyEntityService.UpdateAsync(entity);

            if (!result.Success)
            {
                //TODO Throw Error
            }

            var output = _mapper.Map<AdvanceMoney, AdvanceMoneyOutputResource>(result.Entity);

            return output;
        }


    }
}
