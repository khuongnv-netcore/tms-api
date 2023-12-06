﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CORE_API.CORE.Authorization
{
    internal class SubscriptionPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "SubscriptionProduct";
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public SubscriptionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var subscriptionProduct = policyName.Substring(POLICY_PREFIX.Length);
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new SubscriptionProductRequirement(subscriptionProduct));
                return Task.FromResult(policy.Build());
            }

            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
