using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CORE_API.Tms.Models.Entities;

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
        public List<BookingContainerDetailInputResource> bookingContainerDetails { get; set; }
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
