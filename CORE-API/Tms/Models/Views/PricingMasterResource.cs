using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Views
{
    public class PricingMasterInputResource : CoreInputResource
    {
        [MaxLength(255)]
        public string ProductName { get; set; }
        public EFeeType FeeType { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public List<PricingMasterDetailInputResource> PricingMasterDetails { get; set; }
    }

    public class PricingMasterOutputResource : CoreOutputResource
    {
        [MaxLength(255)]
        public string ProductName { get; set; }
        public EFeeType FeeType { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public List<PricingMasterDetailOutputResource> PricingMasterDetails { get; set; }
    }

    public class UpdatePricingMasterInputResource : CoreInputResource
    {
        [MaxLength(255)]
        public string ProductName { get; set; }
        public EFeeType FeeType { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
