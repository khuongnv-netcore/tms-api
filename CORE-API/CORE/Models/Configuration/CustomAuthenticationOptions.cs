using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Configuration
{
    public class CustomAuthenticationOptions
    {
        public string Secret { get; set; }
        public string AuthenticationMethod { get; set; }
        public int CustomTokenExpirationDay { get; set; }
    }
}
