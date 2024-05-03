using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Views
{
    public class EmployeeInputResource : CoreInputResource
    {
        [MaxLength(200)]
        public string EmployeeName { get; set; } 
        [MaxLength(300)]
        public string EmployeeAddress { get; set; }
        [MaxLength(300)]
        public string EmployeeCode { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Tel { get; set; }
        public DateTime Birthday { get; set; }
        [MaxLength(10)]
        public string Sex { get; set; }
        [MaxLength(50)]
        public string CardNo { get; set; }
        [MaxLength(50)]
        public string TaxCode { get; set; }
        [MaxLength(30)]
        public string CountryCode { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        [MaxLength(50)]
        public string DepartmentCode { get; set; } // Assuming a department exists
        public decimal BasicSalary { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }

    public class EmployeeOutputResource : CoreOutputResource
    {
        [MaxLength(200)]
        public string EmployeeName { get; set; } 
        [MaxLength(300)]
        public string EmployeeAddress { get; set; }
        [MaxLength(300)]
        public string EmployeeCode { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Tel { get; set; }
        public DateTime Birthday { get; set; }
        [MaxLength(10)]
        public string Sex { get; set; }
        [MaxLength(50)]
        public string CardNo { get; set; }
        [MaxLength(50)]
        public string TaxCode { get; set; }
        [MaxLength(30)]
        public string CountryCode { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        [MaxLength(50)]
        public string DepartmentCode { get; set; } 
        public decimal BasicSalary { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
