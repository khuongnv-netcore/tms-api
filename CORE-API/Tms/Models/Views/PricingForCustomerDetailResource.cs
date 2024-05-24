using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System;

namespace CORE_API.Tms.Models.Views
{
    public class PricingForCustomerDetailInputResource : CoreInputResource
    {
        public Guid PricingMasterId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PriceForSale { get; set; }
        public Guid PricingForCustomerId { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }

    public class PricingForCustomerDetailOutputResource : CoreOutputResource
    {
        public Guid PricingMasterId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PriceForSale { get; set; }
        public Guid PricingForCustomerId { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }
}
