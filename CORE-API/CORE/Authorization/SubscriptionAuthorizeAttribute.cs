using System;
using Microsoft.AspNetCore.Authorization;

namespace CORE_API.CORE.Authorization
{
    internal class SubscriptionAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "SubscriptionProduct";
        public SubscriptionAuthorizeAttribute(string subscriptionProduct) => SubscriptionProduct = subscriptionProduct;

        public string SubscriptionProduct
        {
            get
            {
                var subscriptionProduct = Policy.AsSpan(POLICY_PREFIX.Length);
                return subscriptionProduct.ToString();
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value}";
            }
        }
    }
}
