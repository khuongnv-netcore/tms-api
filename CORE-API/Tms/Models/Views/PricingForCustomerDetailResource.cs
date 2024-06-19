using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System;

namespace CORE_API.Tms.Models.Views
{
    public class PricingForCustomerDetailInputResource : CoreInputResource
    {
        public Guid Id { get; set; }
        public Guid PricingMasterDetailId { get; set; }
        public Guid PricingMasterId { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid? FromLocationId { get; set; }
        public Guid? ToLocationId { get; set; }
        public decimal PriceForSale { get; set; }
        public Guid PricingForCustomerId { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }

    public class PricingForCustomerDetailOutputResource : CoreOutputResource
    {
        public Guid PricingMasterDetailId { get; set; }
        public Guid PricingMasterId { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid? FromLocationId { get; set; }
        public Guid? ToLocationId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PriceForSale { get; set; }
        public Guid PricingForCustomerId { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }
}
