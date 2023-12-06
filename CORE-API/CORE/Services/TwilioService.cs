using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using CORE_API.CORE.Models.Configuration;
using Microsoft.Extensions.Options;
using CORE_API.CORE.Models.Views;
using System.Threading.Tasks;
using CORE_API.CORE.Services.Abstract;

namespace CORE_API.CORE.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly CoreConfigurationOptions _coreConfigurationOptions;

        public TwilioService(IOptions<CoreConfigurationOptions> coreConfigurationOption)
        {
            _coreConfigurationOptions = coreConfigurationOption.Value;

            TwilioClient.Init(_coreConfigurationOptions.TwilioOptions.AccountSid, _coreConfigurationOptions.TwilioOptions.AuthToken);

        }

        public Task<SendSmsToUserOutputResource> SendSmsToUser(SendSmsToUserInputResource resource) {

            var sendSmsToUserOutputResource = new SendSmsToUserOutputResource();
            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(string.Format("+{0}", resource.NumberPhone)));

            messageOptions.MessagingServiceSid = _coreConfigurationOptions.TwilioOptions.MessagingServiceSid;

            messageOptions.Body = resource.Body;

            try {
                var message = MessageResource.Create(messageOptions);
                sendSmsToUserOutputResource.Status = message.Status.ToString();
                sendSmsToUserOutputResource.NumberPhone = message.To;
                sendSmsToUserOutputResource.Body = message.Body;
                sendSmsToUserOutputResource.ErrorCode = message.ErrorCode;
                sendSmsToUserOutputResource.ErrorMessage = message.ErrorMessage;
            }
            catch (Twilio.Exceptions.ApiException e) {
                sendSmsToUserOutputResource.ErrorMessage = e.Message;
            }
            

            return Task.FromResult(sendSmsToUserOutputResource);


        }
    }
}
