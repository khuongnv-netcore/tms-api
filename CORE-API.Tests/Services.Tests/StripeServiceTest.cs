using System;
using NUnit.Framework;
using Moq;
using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Stripe;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services.Abstract;
using CORE_API.CORE.Repositories.Abstract;
using CORE_API.CORE.Services;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CORE_API.Tests.Services.Tests
{
    public class StripeServiceTest
    {
        private IStripeService stripeService;
        private Mock<IGenericEntityRepository<User>> userRepository;
        private IOptions<CoreConfigurationOptions> coreConfigurationOptions;
        private User user;
        private string createdSubsriptionId = "";
        private string customerId = "";

        [OneTimeSetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false)
                    .Build();

            coreConfigurationOptions = Options.Create(configuration.GetSection("CoreConfigurationOptions").Get<CoreConfigurationOptions>());

            userRepository = new Mock<IGenericEntityRepository<User>>();

            var userService = new GenericEntityService<User>(userRepository.Object);

            stripeService = new StripeService(coreConfigurationOptions, userService);

            user = new User
            {
                Id = Guid.Parse("05cd28d8-7447-43bc-8bd4-a55df79eb80b"),
                FirstName = "Khuong",
                LastName = "Nguyen",
                EmailAddress = "khuong@gmail.com",
                AuthId = "12345",
                StripeCustomerId = null,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };
        }

        [Test, Order(1)]
        public async Task ConfigureUserInStripe()
        {
            var result = await stripeService.ConfigureUserInStripe(user);
            customerId = user.StripeCustomerId;
            Assert.IsNotNull(result.StripeCustomerId);
        }

        [Test, Order(2)]
        public void GetSupportedProductList()
        {
            var result = stripeService.GetSupportedProductList();
            Assert.AreEqual(2, result.Count);
        }

        [Test, Order(3)]
        public void GetSupportedCouponList()
        {
            var result = stripeService.GetSupportedCouponList();
            Assert.AreEqual(2, result.Count);
        }

        [Test, Order(4)]
        public void GetSupportedPlanList()
        {
            var result = stripeService.GetSupportedPlanList();
            Assert.AreEqual(4, result.Count);
        }

        [Test, Order(5)]
        public void GetCoupon()
        {
            var result = stripeService.GetCoupon("discount-amount-for-premium");
            Assert.IsNotNull(result);
        }

        [Test, Order(6)]
        public void CheckSupportedPlan()
        {
            var result = stripeService.CheckSupportedPlan("plan_K9QLVZOU5UCYfn");
            Assert.IsTrue(result);
        }

        [Test, Order(7)]
        public async Task CreateSubscription()
        {

            var createSubscriptionInputResource = new CreateSubscriptionInputResource
            {
                CustomerId = user.StripeCustomerId,
                PlanId = stripeService.GetSupportedPlanList()[0].Id

            };

            var result = await stripeService.CreateSubscription(createSubscriptionInputResource);

            createdSubsriptionId = result.Id;

            Assert.IsNotNull(result.Id);
        }

        [Test, Order(8)]
        public async Task RetrieveSubscription()
        {
            var result = await stripeService.RetrieveSubscription(createdSubsriptionId);

            Assert.IsNotNull(result.Id);
        }

        [Test, Order(9)]
        public async Task FilterSubscription()
        {
            var filterSubscriptionInputResource = new FilterSubscriptionInputResource
            {
                CustomerId = customerId,
                PlanId = stripeService.GetSupportedPlanList()[0].Id
            };

            var result = await stripeService.FilterSubscription(filterSubscriptionInputResource);

            Assert.IsNotNull(result);
        }

        [Test, Order(10)]
        public async Task GetInvoicesForSubscription()
        {
            var getInvoicesForSubscriptionInputResource = new GetInvoicesForSubscriptionInputResource
            {
                SubscriptionId = createdSubsriptionId,
                CustomerId = customerId
            };

            var result = await stripeService.GetInvoicesForSubscription(getInvoicesForSubscriptionInputResource);

            Assert.IsNotNull(result);
        }

        [Test, Order(11)]
        public async Task UpdateSubscription()
        {
            var updateSubscriptionInputResource = new UpdateSubscriptionInputResource
            {
                SubscriptionId = createdSubsriptionId,
                PlanId = stripeService.GetSupportedPlanList()[0].Id
            };

            var result = await stripeService.UpdateSubscription(updateSubscriptionInputResource);
            Assert.IsNotNull(result.Id);
        }

        [Test, Order(12)]
        public async Task CancelSubscription()
        {
            var cancelSubscriptionInputResource = new CancelSubscriptionInputResource
            {
                SubscriptionId = createdSubsriptionId
            };

            var result = await stripeService.CancelSubscription(cancelSubscriptionInputResource);

            Assert.IsNotNull(result.Id);
        }


    }
}
