using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Views
{
    public class CustomerInputResource : CoreInputResource
    {
        public string LegalName { get; set; }

        public string LanguageName { get; set; }

        public string TaxCode { get; set; }
        [MaxLength(30)]
        public string Address { get; set; }
        public string CountryCode { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string SalesOffice { get; set; }
        public string SalesRepOffice { get; set; }
    }

    public class CustomerOutputResource : CoreOutputResource
    {
        public string LegalName { get; set; }

        public string LanguageName { get; set; }

        public string TaxCode { get; set; }
        [MaxLength(30)]
        public string Address { get; set; }
        public string CountryCode { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string SalesOffice { get; set; }
        public string SalesRepOffice { get; set; }
    }
}
