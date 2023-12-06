using System;

namespace CORE_API.CORE.Models.Views
{
    public class UserRoleInputResource : CoreInputResource
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UserRoleOutputResource : CoreOutputResource
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
