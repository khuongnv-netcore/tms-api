using System.Collections.Generic;

namespace CORE_API.CORE.Models.Views
{
    public class OrganizationInputResource : CoreInputResource
    {
        public string Name { get; set; }
    }

    public class OrganizationOutputResource : CoreOutputResource
    {
        public string Name { get; set; }

    }
}
