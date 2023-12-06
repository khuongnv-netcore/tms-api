using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Views;
using System;
using System.Collections.Generic;

namespace CORE_API.CORE.Models.Views
{
    public class AuthenticationInputResource : CoreInputResource
    {
        public Guid? UserId { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }

    public class AuthenticationOutputResource : CoreOutputResource
    {
        public string Token { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
