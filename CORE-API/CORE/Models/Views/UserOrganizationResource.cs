using CORE_API.CORE.Models.Enums;
using System;

namespace CORE_API.CORE.Models.Views
{
    public class UserOrganizationInputResource : CoreInputResource
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public EOrganizationUserRole OrganizationUserRole { get; set; }
    }

    public class UserOrganizationOutputResource : CoreOutputResource
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public EOrganizationUserRole OrganizationUserRole { get; set; }
    }
}
