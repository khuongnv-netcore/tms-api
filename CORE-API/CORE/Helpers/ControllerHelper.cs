using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CORE_API.CORE.Helpers
{
    public class ControllerHelper : IControllerHelper
    {
        private readonly IGenericEntityService<User> _userService;

        public ControllerHelper(IGenericEntityService<User> userService)
        {
            _userService = userService;
        }

        public User GetCurrentUser(ClaimsPrincipal principal)
        {
            string userId = principal.FindFirstValue(ClaimTypes.PrimarySid);

            var user = _userService.FindOne(x => x.Id.ToString().Equals(userId));

            if(user == null)
            {
                //TODO: Throw Exception
                return null;
            }
            else
            {
                return user;
            }
        }
    }
}
