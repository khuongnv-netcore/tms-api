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
    public class PricingForCustomerInputResource : CoreInputResource
    {
        public DateTime FromDatePeriod { get; set; }
        public DateTime ToDatePeriod { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public string ContractNo { get; set; }

        public List<PricingForCustomerDetailInputResource> PricingForCustomerDetails { get; set; }
    }

    public class PricingForCustomerOutputResource : CoreOutputResource
    {
        public DateTime FromDatePeriod { get; set; }
        public DateTime ToDatePeriod { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public string ContractNo { get; set; }
        public List<PricingForCustomerDetailOutputResource> PricingForCustomerDetails { get; set; }
    }

    public class UpdatePricingForCustomerInputResource : CoreInputResource
    {
        public DateTime FromDatePeriod { get; set; }
        public DateTime ToDatePeriod { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public string ContractNo { get; set; }
    }
}