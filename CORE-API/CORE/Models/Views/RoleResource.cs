using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Views
{
    public class RoleInputResource : CoreInputResource
    {
        public string DisplayName { get; set; }
    }

    public class RoleOutputResource : CoreOutputResource
    { 
        public string DisplayName { get; set; }
    }
}
