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

namespace CORE_API.Tms.Services
{
    public class AutoNumberService
    {
        private IGenericEntityService<AutoNumber> _autoNumberEntityService;
        private IMapper _mapper;

        public AutoNumberService(
                IGenericEntityService<AutoNumber> autoNumberEntityService,
                IMapper mapper
            ) {
            _autoNumberEntityService = autoNumberEntityService;
            _mapper = mapper;
        }

        public async Task<string> getNewEmployeeCode()
        {
            var employeeCode = "";

            var autoNumber = _autoNumberEntityService.FindOne(m => m.AutoNumberType == EAutoNumberType.EMPLOYEE_CODE);

            if (autoNumber != null)
            {

            }

            return employeeCode;
        }


    }
}
