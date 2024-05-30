using System;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CORE_API.Tms.Models.Enums;

namespace CORE_API.Tms.Models.Entities
{
    public class Employee : CoreEntity
    {
        [MaxLength(200)]
        public string EmployeeName { get; set; }
        [MaxLength(300)]
        public string EmployeeAddress { get; set; }
        [MaxLength(50)]
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
        [MaxLength(30)]
        public string DepartmentCode { get; set; }
        public decimal BasicSalary { get; set; } = 0.00m;
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "Employee";

            // Generic
            builder.Entity<Employee>().ToTable(tableName);
            builder.Entity<Employee>().HasKey(m => m.Id);
            builder.Entity<Employee>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().HasIndex(m => m.Created);
            builder.Entity<Employee>().HasIndex(m => m.Modified);
            builder.Entity<Employee>().Property(e => e.BasicSalary).HasPrecision(16, 2);

            builder.Entity<Employee>()
                .HasQueryFilter(m => m.DeletedAt == null);

            // Relationship
        }
        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {

            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {

        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {

            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {

        }
    }
}
