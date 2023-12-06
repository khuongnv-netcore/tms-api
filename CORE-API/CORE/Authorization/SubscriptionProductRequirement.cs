using Microsoft.AspNetCore.Authorization;

namespace CORE_API.CORE.Authorization
{
    public class SubscriptionProductRequirement : IAuthorizationRequirement
    {
        public string SubscriptionProduct { get; private set; }

        public SubscriptionProductRequirement(string subscriptionProduct)
        {
            SubscriptionProduct = subscriptionProduct;
        }
    }
}
