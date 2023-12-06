using CORE_API.CORE.Contexts;
using CORE_API.CORE.Exceptions;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Authorization;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE;

namespace CORE_API.CORE
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly CoreContext _context;
        public readonly IGenericEntityService<User> _userService;
        public IStripeService _stripeService;
        public IMailService _mailService;
        public IControllerHelper _controllerHelper;

        private readonly CoreConfigurationOptions _coreConfigurationOptions;

        public TestController(
                CoreContext context,
                IGenericEntityService<User> userService,
                IOptions<CoreConfigurationOptions> coreConfigurationOptions,
                IStripeService stripeService,
                IMailService mailService,
                IControllerHelper controllerHelper)
        {
            _context = context;
            _userService = userService;
            _coreConfigurationOptions = coreConfigurationOptions.Value;
            _stripeService = stripeService;
            _mailService = mailService;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public Task<User> CreateUserTest()
        {

            throw new ApiDatabaseException("This is a test");

            //var daniel = new User
            //{
            //    FirstName = "Daniel",
            //    LastName = "Litvak",
            //    EmailAddress = "daniel@litvak.com"
            //};

            //var result = await _userService.AddAsync(daniel);

            //return result.Entity;
        }

        [HttpGet("Claims")]
        [Authorize]
        [SwaggerSummary("[DEBUG ONLY] List all claims from token")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }


        [HttpGet("ClaimsAdmin")]
        [Authorize(Roles = "Administrator")]
        [SwaggerSummary("[DEBUG ONLY] List all claims from token")]
        public IActionResult ClaimsAdmin()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }

        [HttpGet("ClaimsUser")]
        [Authorize(Roles = "User")]
        [SwaggerSummary("[DEBUG ONLY] List all claims from token")]
        public IActionResult ClaimsUser()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }

        [HttpGet("ClaimsSupport")]
        [Authorize(Roles = "Support")]
        [SwaggerSummary("[DEBUG ONLY] List all claims from token")]
        public IActionResult ClaimsSupport()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }

        [HttpPost("create-subscription")]
        [SwaggerSummary("create-subscription")]
        public async Task<CreateSubscriptionOutResource> CreateSubscription([FromBody] CreateSubscriptionInputResource req)
        {

            // Create subscription

            var subscription = await _stripeService.CreateSubscription(req);

            return new CreateSubscriptionOutResource
            {
                SubscriptionId = subscription.Id,
                ClientSecret = subscription.LatestInvoice.PaymentIntent.ClientSecret,
            };
        }

        [HttpPost("update-subscription")]
        [SwaggerSummary("update-subscription")]
        public async Task<UpdateSubscriptionOutputResource> UpdateSubscription([FromBody] UpdateSubscriptionInputResource req)
        {

            // Update subscription

            var subscription = await _stripeService.UpdateSubscription(req);

            return new UpdateSubscriptionOutputResource
            {
                SubscriptionId = subscription.Id
            };
        }

        [HttpGet("GetConfigureStripe")]
        [SwaggerSummary("GetConfigureStripe")]
        [Authorize(Roles = "Administrator")]
        [SubscriptionAuthorize("Premium User")]
        public GetConfigureStripeOutputResource GetConfigureStripe()
        {

            var products =  _stripeService.GetSupportedProductList();
            var coupons = _stripeService.GetSupportedCouponList();

            var configureStripeOutputResource = new GetConfigureStripeOutputResource();

            configureStripeOutputResource.Products = products;
            configureStripeOutputResource.Coupons = coupons;

            return configureStripeOutputResource;
        }

        [HttpDelete("DeleteMailTemplate")]
        [SwaggerSummary("DeleteMailTemplate")]
        public  IActionResult DeleteMailTemplate(string templateName)
        {
            _mailService.DeleteMailTemplate(templateName);

            return Ok();
        }

        [HttpPost("CreateMailTemplate")]
        [SwaggerSummary("CreateMailTemplate")]
        public IActionResult CreateMailTemplate(string templateName, string subject)
        {
            _mailService.CreateMailTemplate(templateName, subject);

            return Ok();
        }

        [HttpPost("SendMailThankYouCreateSubscription")]
        [SwaggerSummary("SendMailThankYouCreateSubscription")]
        [Authorize]
        public IActionResult SendMailThankYouCreateSubscription()
        {
            var user = _controllerHelper.GetCurrentUser(User);

            if (user == null)
            {
                throw new Exception("User not found!");
            }

            var templateData = new
            {
                fullName = string.Format("{0} {1}", user.FirstName, user.LastName),
                baseUrl = _coreConfigurationOptions.BaseUrl
            };

            List<string> toAddresses = new List<string> {
                user.EmailAddress
            };

            //_mailService.SendMailForTemplate(Constants.EMAIL_TEMPLATE_THANK_YOU_CREATE_SUBSCRIPTION, templateData, toAddresses);
            _mailService.SendMailThankYouCreateSubscription(user);
            
            return Ok();
        }
    }
}
