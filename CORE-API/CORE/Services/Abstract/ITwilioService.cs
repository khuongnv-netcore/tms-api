using System;
using Twilio.Rest.Api.V2010.Account;
using CORE_API.CORE.Models.Views;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services.Abstract
{
    public interface ITwilioService
    {
        Task<SendSmsToUserOutputResource> SendSmsToUser(SendSmsToUserInputResource resource);
    }
}
