using System;
using System.Collections.Generic;

namespace CORE_API.CORE.Models.Views
{
    public class CreateSubscriptionInputResource
    {
        public string CustomerId { get; set; }
        public string PlanId { get; set; }
        public string CouponId{ get; set; }
    }

    public class CreateSubscriptionOutResource
    {
        public string SubscriptionId { get; set; }
        public string ClientSecret { get; set; }
        public string PlanId { get; set; }
        public string CouponId { get; set; }
    }

    public class UpdateSubscriptionInputResource
    {
        public string SubscriptionId { get; set; }
        public string PlanId { get; set; }
    }

    public class UpdateSubscriptionOutputResource
    {
        public string SubscriptionId { get; set; }
    }

    public class CancelSubscriptionInputResource
    {
        public string SubscriptionId { get; set; }
    }

    public class CancelSubscriptionOutputResource
    {
        public string SubscriptionId { get; set; }
    }

    public class RetrieveSubscriptionInputResource
    {
        public string SubscriptionId { get; set; }
    }

    public class RetrieveSubscriptionOutputResource
    {
        public string SubscriptionId { get; set; }
        public string PlanId { get; set; }
        public string ProductId { get; set; }
        public List<InvoiceOutputResource> Invoices { get; set; }
    }

    public class InvoiceOutputResource
    {
        public string Id { get; set; }
        public long AmountDue { get; set; }
        public long AmountPaid { get; set; }
        public long AmountRemaining { get; set; }
        public long Subtotal { get; set; }
        public long Total { get; set; }
        public bool PaidStatus { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
    }

    public class FilterSubscriptionInputResource
    {
        public string CustomerId { get; set; }
        public string PlanId { get; set; }
    }

    public class SupportedProductOutputResource {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SupportedPlanOutputResource> Plans { get; set; }

        public SupportedProductOutputResource() {
            Plans = new List<SupportedPlanOutputResource>();
        }
    }

    public class SupportedPlanOutputResource {
        public string Id { get; set; }
        public long? Amount { get; set; }
        public string Currency { get; set; }
        public string Interval { get; set; }
        public string Nickname { get; set; }
        public int SubscriptionRenewalExtensionPeriod_Months { get; set; }
    }

    public class SupportedCouponOutputResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? PercentOff { get; set; }
        public long? AmountOff { get; set; }
        public string Currency { get; set; }
        public string Duration { get; set; }
        public long? DurationInMonths { get; set; }
        public DateTime? RedeemBy { get; set; }
    }

    public class GetConfigureStripeOutputResource
    {
        public List<SupportedProductOutputResource> Products { get; set; }
        public List<SupportedCouponOutputResource> Coupons { get; set; }
    }

    public class GetStripeProductsOutputResource
    {
        public List<SupportedProductOutputResource> Products { get; set; }
    }

    public class GetInvoicesForSubscriptionInputResource
    {
        public string SubscriptionId { get; set; }
        public string CustomerId { get; set; }
    }
}
