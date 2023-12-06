using System;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Views;
using System.Threading.Tasks;
using Stripe;
using System.Collections.Generic;

namespace CORE_API.CORE.Services.Abstract
{
    public interface IStripeService
    {
        Task<User> ConfigureUserInStripe(User user);
        Task<Subscription> CreateSubscription(CreateSubscriptionInputResource resource);
        Task<Subscription> UpdateSubscription(UpdateSubscriptionInputResource resource);
        Task<Subscription> CancelSubscription(CancelSubscriptionInputResource resource);
        Task<Subscription> RetrieveSubscription(string subscriptionId);
        Task<StripeList<Subscription>> FilterSubscription(FilterSubscriptionInputResource resource);
        List<SupportedProductOutputResource> GetSupportedProductList();
        List<SupportedCouponOutputResource> GetSupportedCouponList();
        List<SupportedPlanOutputResource> GetSupportedPlanList();
        SupportedCouponOutputResource GetCoupon(string couponId);
        Task <List<InvoiceOutputResource>> GetInvoicesForSubscription(GetInvoicesForSubscriptionInputResource resource);
        bool CheckSupportedPlan(string planId);
    }
}
