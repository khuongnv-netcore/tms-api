using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Configuration
{
    public class AuthenticationOptions
    {
        public string ApiIdentifier { get; set; }
        public string Domain { get; set; }
        public string Audience { get; set; }
        public string AuthorizationUrl { get; set; }
        public string TokenUrl { get; set; }
        public string ApiClientId { get; set; }

        // Config for Auth0 Management
        public string ManagementHostname { get; set; }
        public string ManagementAudience { get; set; }
        public string ManagementClientSecret { get; set; }

        public ClaimsOptions ClaimsOptions {get; set;}

        public string getAuthorizationWithAudienceUrl()
        {
            return AuthorizationUrl + "?audience=" + Audience;
        }
    }
}
