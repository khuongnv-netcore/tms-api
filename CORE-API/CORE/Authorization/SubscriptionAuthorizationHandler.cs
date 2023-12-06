using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CORE_API.CORE.Authorization
{
    internal class SubscriptionAuthorizationHandler : AuthorizationHandler<SubscriptionProductRequirement>
    {
        IHttpContextAccessor _httpContextAccessor = null;

        public SubscriptionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SubscriptionProductRequirement requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            var subscriptionProduct = context.User.FindFirstValue("SubscriptionProduct");

            var subscriptionEndDate = context.User.FindFirstValue("SubscriptionEndDate");

            if ((!string.IsNullOrEmpty(subscriptionProduct) && subscriptionProduct == requirement.SubscriptionProduct)
                && (!string.IsNullOrEmpty(subscriptionEndDate)) && Convert.ToDateTime(subscriptionEndDate) > DateTime.UtcNow
                )
            {
                context.Succeed(requirement);
            }
            else {
                context.Fail();
                httpContext.Response.Headers.Add("RequireSubscriptionProduct", requirement.SubscriptionProduct);
            }

            return Task.CompletedTask;
        }
    }
}
