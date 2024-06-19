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
    public class AdvanceMoneyInputResource : CoreInputResource
    {
        public Guid? BookingId { get; set; }
        public Guid? EmployeeId { get; set; }
        public decimal Money { get; set; }
        public string Note { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public List<AdvanceMoneyDocumentInputResource> AdvanceMoneyDocuments { get; set; }
    }

    public class AdvanceMoneyOutputResource : CoreOutputResource
    {
        public Guid? BookingId { get; set; }
        public string BookingNo { get; set; }
        public Guid? EmployeeId { get; set; }
        public decimal Money { get; set; }
        public string Note { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public List<AdvanceMoneyDocumentOutputResource> AdvanceMoneyDocuments { get; set; }
    }

    public class UpdateAdvanceMoneyInputResource : CoreInputResource
    {
        public Guid? BookingId { get; set; }
        public Guid? EmployeeId { get; set; }
        public decimal Money { get; set; }
        public string Note { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
    
}