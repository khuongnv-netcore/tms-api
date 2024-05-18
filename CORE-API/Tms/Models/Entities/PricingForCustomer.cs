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
using System.Reflection.Emit;

namespace CORE_API.Tms.Models.Entities
{
    public class PricingForCustomer : CoreEntity
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CustomerId {  get; set; }
        public string CustomerName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string PricingMasterId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; } = 0.00m;
        public decimal SalePrice { get; set; } = 0.00m;
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "PricingForCustomer";

            // Generic
            builder.Entity<PricingForCustomer>().ToTable(tableName);
            builder.Entity<PricingForCustomer>().HasKey(m => m.Id);
            builder.Entity<PricingForCustomer>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PricingForCustomer>().HasIndex(m => m.Created);
            builder.Entity<PricingForCustomer>().HasIndex(m => m.Modified);

            builder.Entity<PricingForCustomer>()
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
