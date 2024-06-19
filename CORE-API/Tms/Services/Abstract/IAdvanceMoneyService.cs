using CORE_API.CORE.Models.Views;
using CORE_API.Tms.Models.Entities;
using CORE_API.Tms.Models.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CORE_API.Tms.Services.Abstract
{
    public interface IAdvanceMoneyService
    {
       Task<AdvanceMoneyOutputResource> updateAdvanceMoney(Guid id, AdvanceMoneyInputResource resource);
    }
}
