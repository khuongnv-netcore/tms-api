using System;
using CORE_API.CORE.Models.Views;
using System.Collections.Generic;
using CORE_API.CORE.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Stripe;
using System.Linq;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services.Abstract;

namespace CORE_API.CORE.Services
{
    public class StripeService : IStripeService
    {
        protected List<SupportedProductOutputResource> _supportedProducts;
        protected List<SupportedCouponOutputResource> _supportedCoupons;
        protected List<SupportedPlanOutputResource> _supportedPlans;
        private readonly CoreConfigurationOptions _coreConfigurationOptions;
        private readonly CustomerService _customerService;
        public IGenericEntityService<User> _userEntityService;

        public StripeService(
            IOptions<CoreConfigurationOptions> coreConfigurationOption,
            IGenericEntityService<User> userEntityService)
        {
            _coreConfigurationOptions = coreConfigurationOption.Value;
            _supportedProducts = new List<SupportedProductOutputResource>();
            _supportedCoupons = new List<SupportedCouponOutputResource>();
            _supportedPlans = new List<SupportedPlanOutputResource>();
            _customerService = new CustomerService();
            _userEntityService = userEntityService;
            StripeConfiguration.ApiKey = _coreConfigurationOptions.StripeOptions.SecretKey;
            this.ConfigureStripe().Wait();
        }

        public async Task<User> ConfigureUserInStripe(User user)
        {
            if (string.IsNullOrEmpty(user.StripeCustomerId))
            {
                var options = new CustomerCreateOptions
                {
                    Email = user.EmailAddress,
                    Name = user.GetFullName(),
                    Metadata = new Dictionary<string, string>
                    {
                        {"ApiId", user.Id.ToString() }
                    }
                };
                Customer customer = _customerService.Create(options);
                user.StripeCustomerId = customer.Id;

                var result = await _userEntityService.UpdateAsync(user);

                return result.Entity;

            }

            return user;
        }

        public async Task<Subscription> CreateSubscription(CreateSubscriptionInputResource resource)
        {
            if (!this.CheckSupportedPlan(resource.PlanId)) {
                throw new Exception("Plan is not supported");
            }

            if (!this.CheckSupportedCoupon(resource.CouponId))
            {
                throw new Exception("Coupon is not supported");
            }

            var subscriptionOptions = new SubscriptionCreateOptions();

            subscriptionOptions.Customer = resource.CustomerId;
            subscriptionOptions.PaymentBehavior = "default_incomplete";
            if (!string.IsNullOrEmpty(resource.CouponId)) {
                subscriptionOptions.Coupon = resource.CouponId;
            }
            subscriptionOptions.Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = resource.PlanId,
                    },
                };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            var subscriptionService = new SubscriptionService();
            try
            {
                var subscription = await subscriptionService.CreateAsync(subscriptionOptions);

                return subscription;
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Failed to create subscription.{e}");
                throw new Exception("Bad request");
            }
        }

        public async Task<Subscription> UpdateSubscription(UpdateSubscriptionInputResource resource)
        {
            if (!this.CheckSupportedPlan(resource.PlanId))
            {
                throw new Exception("Plan is not supported");
            }

            var subscriptionService = new SubscriptionService();

            Subscription subscription = subscriptionService.Get(resource.SubscriptionId);

            var items = new List<SubscriptionItemOptions> {
                            new SubscriptionItemOptions {
                                Id = subscription.Items.Data[0].Id,
                                Price = resource.PlanId,
                            },
                        };

            var subscriptionOptions = new SubscriptionUpdateOptions
            {
                CancelAtPeriodEnd = false,
                ProrationBehavior = "create_prorations",
                Items = items,
            };

            subscription = await subscriptionService.UpdateAsync(resource.SubscriptionId, subscriptionOptions);

            return subscription;
        }

        public async Task<Subscription> CancelSubscription(CancelSubscriptionInputResource resource)
        {
            var subscriptionService = new SubscriptionService();

            var subscription = await subscriptionService.CancelAsync(resource.SubscriptionId);

            return subscription;
        }

        public async Task<Subscription> RetrieveSubscription(string subscriptionId)
        {
            var subscriptionService = new SubscriptionService();

            var subscription = await subscriptionService.GetAsync(subscriptionId);

            return subscription;
        }

        public async Task<StripeList<Subscription>> FilterSubscription(FilterSubscriptionInputResource resource) {

            var subscriptionService = new SubscriptionService();

            var options = new SubscriptionListOptions();
            if (!string.IsNullOrEmpty(resource.CustomerId)) {
                options.Customer = resource.CustomerId;
            }

            if (!string.IsNullOrEmpty(resource.PlanId))
            {
                options.Price = resource.PlanId;
            }

            StripeList<Subscription> subscriptions = await subscriptionService.ListAsync(
              options
            );

            return subscriptions;
        }

        public List<SupportedProductOutputResource> GetSupportedProductList() {
            return _supportedProducts;
        }

        public List<SupportedCouponOutputResource> GetSupportedCouponList()
        {
            return _supportedCoupons;
        }

        public List<SupportedPlanOutputResource> GetSupportedPlanList()
        {
            return _supportedPlans;
        }

        public SupportedCouponOutputResource GetCoupon(string couponId) {
            
            var supportedCouponOutputResource = _supportedCoupons.FirstOrDefault(x => x.Id.Equals(couponId, StringComparison.InvariantCultureIgnoreCase));

            return supportedCouponOutputResource;
        }

        public async Task<List<InvoiceOutputResource>> GetInvoicesForSubscription(GetInvoicesForSubscriptionInputResource resource) {

            var invoicesForSubscription = new List<InvoiceOutputResource>();
            var options = new InvoiceListOptions
            {
                Limit = 0,
                Customer = resource.CustomerId,
                Subscription = resource.SubscriptionId
            };

            var subscriptionService = new InvoiceService();
            StripeList<Invoice> invoices = await subscriptionService.ListAsync(
              options
            );

            foreach (var invoice in invoices) {
                var invoiceOutputResource = new InvoiceOutputResource();
                invoiceOutputResource.Id = invoice.Id;
                invoiceOutputResource.AmountDue = invoice.AmountDue;
                invoiceOutputResource.AmountPaid = invoice.AmountPaid;
                invoiceOutputResource.AmountRemaining = invoice.AmountRemaining;
                invoiceOutputResource.Subtotal = invoice.Subtotal;
                invoiceOutputResource.Total = invoice.Total;
                invoiceOutputResource.Status = invoice.Status;
                invoiceOutputResource.PaidStatus = invoice.Paid;
                invoiceOutputResource.CustomerId = invoice.CustomerId;
                invoiceOutputResource.Created = invoice.Created;
                invoicesForSubscription.Add(invoiceOutputResource);
            }

            return invoicesForSubscription;
        }

        public bool CheckSupportedPlan(string planId)
        {
            return _supportedPlans.Any(x => x.Id.Equals(planId, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool CheckSupportedCoupon(string couponId)
        {
            if (string.IsNullOrEmpty(couponId)) return true;

            return _supportedCoupons.Any(x => x.Id.Equals(couponId, StringComparison.InvariantCultureIgnoreCase));
        }

        private async Task ConfigureStripe()
        {
            var productService = new ProductService();
            var planService = new PlanService();
            var couponService = new CouponService();

            var products = await productService.ListAsync();
            var plans = await planService.ListAsync();
            var coupons = await couponService.ListAsync();

            foreach (var product in _coreConfigurationOptions.StripeOptions.Products.ToList())
            {
                var supportedProduct = new SupportedProductOutputResource();

                var foundProduct = products.FirstOrDefault(r => r.Name == product.Name);

                if (foundProduct == null)
                {
                    var productOptions = new ProductCreateOptions
                    {
                        Name = product.Name
                    };

                    var newProduct = await productService.CreateAsync(productOptions);
                    supportedProduct.Id = newProduct.Id;
                    supportedProduct.Name = newProduct.Name;
                }
                else {
                    supportedProduct.Id = foundProduct.Id;
                    supportedProduct.Name = foundProduct.Name;
                }

                foreach (var plan in product.Plans)
                {
                    var supportedPlan = new SupportedPlanOutputResource();
         
                    var foundPlan = plans.FirstOrDefault(r => r.Nickname == plan.Nickname);
                    if (foundPlan == null)
                    {
                        var newPlan = await planService.CreateAsync(new PlanCreateOptions
                        {
                            Nickname = plan.Nickname,
                            Amount = plan.Amount,
                            Product = supportedProduct.Id,
                            Interval = plan.Interval,
                            Currency = plan.Currency
                        });
                        supportedPlan.Id = newPlan.Id;
                        supportedPlan.Nickname = newPlan.Nickname;
                        supportedPlan.Interval = newPlan.Interval;
                        supportedPlan.Amount = newPlan.Amount;
                        supportedPlan.Currency = newPlan.Currency;
                        supportedPlan.SubscriptionRenewalExtensionPeriod_Months = plan.SubscriptionRenewalExtensionPeriod_Months;
                    }
                    else {
                        supportedPlan.Id = foundPlan.Id;
                        supportedPlan.Nickname = foundPlan.Nickname;
                        supportedPlan.Interval = plan.Interval;
                        supportedPlan.Amount = plan.Amount;
                        supportedPlan.Currency = plan.Currency;
                        supportedPlan.SubscriptionRenewalExtensionPeriod_Months = plan.SubscriptionRenewalExtensionPeriod_Months;
                    }

                    supportedProduct.Plans.Add(supportedPlan);
                    _supportedPlans.Add(supportedPlan);
                }
                
                _supportedProducts.Add(supportedProduct);
            }

            foreach (var coupon in _coreConfigurationOptions.StripeOptions.Coupons) {
                
                var foundCoupon = coupons.FirstOrDefault(r => r.Id == coupon.Id);
                var supportedCoupon = new SupportedCouponOutputResource();
                if (foundCoupon == null)
                {
                    var options = new CouponCreateOptions();
                    options.Id = coupon.Id;
                    options.Name = coupon.Name;
                    if (coupon.PercentOff != null) {
                        options.PercentOff = coupon.PercentOff;
                    }
                    if (coupon.AmountOff != null)
                    {
                        options.AmountOff = coupon.AmountOff;
                    }
                    options.Currency = coupon.Currency;

                    options.Duration = coupon.Duration;

                    if (coupon.DurationInMonths != null)
                    {
                        options.DurationInMonths = coupon.DurationInMonths;
                    }

                    if (coupon.RedeemBy != null)
                    {
                        options.RedeemBy = coupon.RedeemBy;
                    }

                    var newCoupon = await couponService.CreateAsync(options);

                    supportedCoupon.Id = newCoupon.Id;
                    supportedCoupon.Name = newCoupon.Name;
                    supportedCoupon.PercentOff = newCoupon.PercentOff;
                    supportedCoupon.AmountOff = newCoupon.AmountOff;
                    supportedCoupon.Currency = newCoupon.Currency;
                    supportedCoupon.Duration = newCoupon.Duration;
                    supportedCoupon.DurationInMonths = newCoupon.DurationInMonths;
                    supportedCoupon.RedeemBy = newCoupon.RedeemBy;

                }
                else {
                    supportedCoupon.Id = foundCoupon.Id;
                    supportedCoupon.Name = foundCoupon.Name;
                    supportedCoupon.PercentOff = foundCoupon.PercentOff;
                    supportedCoupon.AmountOff = foundCoupon.AmountOff;
                    supportedCoupon.Currency = foundCoupon.Currency;
                    supportedCoupon.Duration = foundCoupon.Duration;
                    supportedCoupon.DurationInMonths = foundCoupon.DurationInMonths;
                    supportedCoupon.RedeemBy = foundCoupon.RedeemBy;
                }

                _supportedCoupons.Add(supportedCoupon);
            }
        }
    }
}
