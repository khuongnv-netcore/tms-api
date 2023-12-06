using CORE_API.CORE.Models.Entities;
using System;
using System.Collections.Generic;

namespace CORE_API.CORE.Models.Views
{
    public class UserInputResource : CoreInputResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailVerified { get; set; }
    }

    public class UserOutputResource : CoreOutputResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailVerified { get; set; }

        public List<RoleOutputResource> Roles { get; set; }
        public List<OrganizationOutputResource> Organizations { get; set; }
    }

    public class SetRolesResource
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
