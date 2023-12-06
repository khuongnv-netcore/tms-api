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
using Stripe;

namespace CORE_API.CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : Controller
    {
        public IControllerHelper _controllerHelper;
        public IGenericEntityService<User> _entityService;
        private readonly CoreConfigurationOptions _coreConfigurationOptions;
        public IStripeService _stripeService;
        public IMailService _mailService;

        public StripeController(
            IControllerHelper controllerHelper,
            IGenericEntityService<User> entityService,
            IOptions<CoreConfigurationOptions> coreConfigurationOptions,
            IStripeService stripeService,
            IMailService mailService)
        {
            _controllerHelper = controllerHelper;
            _entityService = entityService;
            _coreConfigurationOptions = coreConfigurationOptions.Value;
            _stripeService = stripeService;
            _mailService = mailService;

        }

        [HttpGet("Products")]
        [SwaggerSummary("Products")]
        [Authorize]
        public ActionResult<GetStripeProductsOutputResource> GetProducts()
        {
            var products = _stripeService.GetSupportedProductList();

            var stripeProductsOutputResource = new GetStripeProductsOutputResource();

            stripeProductsOutputResource.Products = products;

            return stripeProductsOutputResource;
        }

        [HttpGet("Coupons/{code}")]
        [SwaggerSummary("Coupons/{code}")]
        [Authorize]
        public ActionResult<SupportedCouponOutputResource> GetCoupon(string code)
        {
            return _stripeService.GetCoupon(code);
        }

        [HttpPost("create-subscription")]
        [SwaggerSummary("create-subscription")]
        [Authorize]
        public async Task<CreateSubscriptionOutResource> CreateSubscription([FromBody] CreateSubscriptionInputResource req)
        {
            var user = _controllerHelper.GetCurrentUser(User);

            if (user == null)
            {
                throw new Exception("User not found!");
            }
            
            await _stripeService.ConfigureUserInStripe(user);

            // Create subscription
            req.CustomerId = user.StripeCustomerId;
            var subscription = await _stripeService.CreateSubscription(req);

            return new CreateSubscriptionOutResource
            {
                SubscriptionId = subscription.Id,
                ClientSecret = subscription.LatestInvoice.PaymentIntent.ClientSecret,
                PlanId = req.PlanId,
                CouponId = req.CouponId
            };
        }

        [HttpPut("update-subscription")]
        [SwaggerSummary("update-subscription")]
        [Authorize]
        public async Task<UpdateSubscriptionOutputResource> UpdateSubscription([FromBody] UpdateSubscriptionInputResource req)
        {

            // Update subscription

            var subscription = await _stripeService.UpdateSubscription(req);

            return new UpdateSubscriptionOutputResource
            {
                SubscriptionId = subscription.Id
            };
        }

        [HttpDelete("cancel-subscription")]
        [SwaggerSummary("update-subscription")]
        [Authorize]
        public async Task<CancelSubscriptionOutputResource> CancelSubscription([FromBody] CancelSubscriptionInputResource req)
        {

            // Cancel subscription

            var subscription = await _stripeService.CancelSubscription(req);

            return new CancelSubscriptionOutputResource
            {
                SubscriptionId = subscription.Id
            };
        }

        [HttpGet("subscriptions/{subscriptionId}")]
        [SwaggerSummary("subscriptions/{subscriptionId}")]
        [Authorize]
        public async Task<RetrieveSubscriptionOutputResource> RetrieveSubscription(string subscriptionId)
        {

            // Retrieve subscription

            var subscription = await _stripeService.RetrieveSubscription(subscriptionId);

            var getInvoicesForSubscriptionInputResource = new GetInvoicesForSubscriptionInputResource {
                CustomerId = subscription.CustomerId,
                SubscriptionId = subscription.Id
            };

            return new RetrieveSubscriptionOutputResource
            {
                SubscriptionId = subscription.Id,
                PlanId = subscription.Items.Data[0].Price.Id,
                ProductId = subscription.Items.Data[0].Price.ProductId,
                Invoices = await _stripeService.GetInvoicesForSubscription(getInvoicesForSubscriptionInputResource)
            };
        }

        [HttpPost("webhook")]
        [SwaggerSummary("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            Event stripeEvent;
            try
            {
                // Check Product/Plan is supported or not.
                string eventType = Convert.ToString(JObject.Parse(json)["data"]["type"]) != null ? Convert.ToString(JObject.Parse(json)["data"]["type"]) : "";
                if (eventType == "invoice.payment_succeeded")
                {
                    var items = JObject.Parse(json)["data"]["object"]["lines"]["data"];
                    string priceId = Convert.ToString(items[0]["plan"]["id"]) != null ? Convert.ToString(items[0]["plan"]["id"]) : "";

                    bool checkSupportedProductPlan = _stripeService.CheckSupportedPlan(priceId);

                    if (!checkSupportedProductPlan)
                    {
                        return Ok();
                    }
                }

                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _coreConfigurationOptions.StripeOptions.WebhookSecret, 300, false
                );

            }
            catch (Exception e)
            {
                throw new ApiBadRequestException(e.Message, e.InnerException);
            }

            if (stripeEvent.Type == "invoice.payment_succeeded")
            {
                var invoice = stripeEvent.Data.Object as Invoice;

                if (invoice.BillingReason == "subscription_create")
                {
                    // The subscription automatically activates after successful payment
                    // Set the payment method used to pay the first invoice
                    // as the default payment method for that subscription

                    // Retrieve the payment intent used to pay the subscription
                    var service = new PaymentIntentService();
                    var paymentIntent = service.Get(invoice.PaymentIntentId);

                    // Set the default payment method
                    var options = new SubscriptionUpdateOptions
                    {
                        DefaultPaymentMethod = paymentIntent.PaymentMethodId,
                    };
                    var subscriptionService = new SubscriptionService();
                    subscriptionService.Update(invoice.SubscriptionId, options);
                }

                var customerId = invoice.CustomerId;
                var user = _entityService.FindOne(x => x.StripeCustomerId == customerId);

                if (user != null)
                {
                    var subscription = await _stripeService.RetrieveSubscription(invoice.SubscriptionId);
                    var planId = subscription.Items.Data[0].Plan.Id;
                    var supportedPlanList = _stripeService.GetSupportedPlanList();
                    var subscriptionRenewalExtensionPeriod_Months = supportedPlanList.Find(r => r.Id == planId).SubscriptionRenewalExtensionPeriod_Months;

                    // Update SubscriptionProduct and SubscriptionEndDate
                    DateTime usedExpireDateForPayment = DateTime.UtcNow.AddMonths(subscriptionRenewalExtensionPeriod_Months);

                    user.SubscriptionProduct = _stripeService.GetSupportedProductList().Find(r => r.Id == subscription.Items.Data[0].Plan.ProductId).Name;

                    user.SubscriptionEndDate = usedExpireDateForPayment;

                    await _entityService.UpdateAsync(user);

                    //_mailService.SendMailThankYouCreateSubscription(user);
                }
            }

            return Ok();
        }
    }
}
