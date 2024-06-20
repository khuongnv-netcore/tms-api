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
    public class BookingChargeInputResource : CoreInputResource
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid? FromLocationId { get; set; }
        public Guid? ToLocationId { get; set; }
        public double UnitPrice { get; set; }
        public int Vol { get; set; } = 0;
        public double Amount { get; set; } = 0;
        public Guid? PricingForCustomerDetailId { get; set; }
        public Guid? AdvanceMoneyDocumentId { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }

    public class BookingChargeOutputResource : CoreOutputResource
    {
        public Guid BookingId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid? FromLocationId { get; set; }
        public Guid? ToLocationId { get; set; }
        public double UnitPrice { get; set; }
        public int Vol { get; set; } = 0;
        public double Amount { get; set; } = 0;
        public Guid? PricingForCustomerDetailId { get; set; }
        public Guid? AdvanceMoneyDocumentId { get; set; }
        public string DocumentName { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }
}
