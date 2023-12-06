using CORE_API.CORE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services.Abstract
{
    public interface IAuth0ManagementService
    {
        void configure();

        Task<bool> ChangeUserEmailAddress(User user, string EmailAddress);
        Task<bool> SetUserName(User user);
    }
}
