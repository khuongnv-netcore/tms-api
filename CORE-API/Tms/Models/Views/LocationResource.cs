using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Views
{
    public class LocationInputResource : CoreInputResource
    {
        [MaxLength(30)]
        public string NodeCode { get; set; }
        [MaxLength(255)]
        public string NodeName { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }

    public class LocationOutputResource : CoreOutputResource
    {
        [MaxLength(30)]
        public string NodeCode { get; set; }
        [MaxLength(255)]
        public string NodeName { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set;}
    }
}
