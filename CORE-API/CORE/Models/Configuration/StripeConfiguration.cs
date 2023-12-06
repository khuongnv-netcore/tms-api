using System;
using System.Collections.Generic;

namespace CORE_API.CORE.Models.Configuration
{
    public class StripeOptions
    {
        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }
        public string WebhookSecret { get; set; }
        public List<StripeProduct> Products { get; set; }
        public List<StripeCoupon> Coupons { get; set; }
    }

    public class StripeProduct {
        public string Name { get; set; }
        public List<Plan> Plans { get; set; }
    }

    public class Plan {
        public long? Amount { get; set; }
        public string Currency { get; set; }
        public string Interval { get; set; }
        public string Nickname { get; set; }
        public int SubscriptionRenewalExtensionPeriod_Months { get; set; }
    }

    public class StripeCoupon {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? PercentOff { get; set; }
        public long? AmountOff { get; set; }
        public string Currency { get; set; }
        public string Duration { get; set; }
        public long? DurationInMonths { get; set; }
        public DateTime? RedeemBy { get; set; }
    }
}
