using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Configuration
{
    public class CoreConfigurationOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public AuthenticationOptions AuthenticationOptions { get; set; }

        public CustomAuthenticationOptions CustomAuthenticationOptions { get; set; }

        public SwaggerOptions SwaggerOptions { get; set; }

        public LoggingOptions LoggingOptions {get; set;}

        public string Version => GetType().Assembly.GetName().Version.ToString(3);

        public string Name => GetType().Assembly.GetName().Name;

        public StripeOptions StripeOptions { get; set; }

        public TwilioOptions TwilioOptions { get; set; }

        public string BaseUrl { get; set; }

        public AWSConfigOptions AWSConfigOptions { get; set; }
    }
}
