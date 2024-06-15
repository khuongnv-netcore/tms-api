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
    public class AutoNumberInputResource : CoreInputResource
    {
        public EAutoNumberType AutoNumberType { get; set; }
        public string Prefix { get; set; }
        public string RegExp { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int CurrentNumber { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }

    public class AutoNumberOutputResource : CoreOutputResource
    {
        public EAutoNumberType AutoNumberType { get; set; }
        public string Prefix { get; set; }
        public string RegExp { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int CurrentNumber { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}