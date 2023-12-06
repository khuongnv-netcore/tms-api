using CORE_API.CORE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CORE_API.CORE.Helpers.Abstract
{
    public interface IControllerHelper
    {
        User GetCurrentUser(ClaimsPrincipal principal);
    }
}
