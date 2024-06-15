using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System;

namespace CORE_API.Tms.Models.Views
{
    public class AdvanceMoneyDocumentInputResource : CoreInputResource
    {
        public Guid AdvanceMoneyId { get; set; }
        public decimal Money { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFilePath { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }

    public class AdvanceMoneyDocumentOutputResource : CoreOutputResource
    {
        public Guid AdvanceMoneyId { get; set; }
        public decimal Money { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFilePath { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
    }
}
