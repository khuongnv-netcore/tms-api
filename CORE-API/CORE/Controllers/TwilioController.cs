using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Services.Abstract;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Exceptions;
using Newtonsoft.Json.Linq;
using CORE_API.CORE.Models.Views;
using Twilio.Rest.Api.V2010.Account;

namespace CORE_API.CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioController : Controller
    {
        public IControllerHelper _controllerHelper;
        public IGenericEntityService<User> _entityService;
        private readonly CoreConfigurationOptions _coreConfigurationOptions;
        public ITwilioService _twilioService;

        public TwilioController(
            IControllerHelper controllerHelper,
            IGenericEntityService<User> entityService,
            IOptions<CoreConfigurationOptions> coreConfigurationOptions,
            ITwilioService twilioService)
        {
            _controllerHelper = controllerHelper;
            _entityService = entityService;
            _coreConfigurationOptions = coreConfigurationOptions.Value;
            _twilioService = twilioService;

        }

        [HttpPost("SendSmsToUser")]
        [SwaggerSummary("SendSmsToUser")]
        [Authorize]
        public ActionResult<SendSmsToUserOutputResource> SendSmsToUser(SendSmsToUserInputResource resource)
        {
            var sendSmsToUserOutputResource = _twilioService.SendSmsToUser(resource);

            return sendSmsToUserOutputResource.Result;
        }
    }
}
