using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Configuration
{
    public class ClaimsOptions
    {
        public string Namespace { get; set; }
        public string EmailAddress { get; set; } 

        public string EmailVerified { get; set; }

        
    }
}
