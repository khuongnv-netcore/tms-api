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
    public class PricingMaster : CoreEntity
    {
        [MaxLength(255)]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; } = 0.00m;
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "PricingMaster";

            // Generic
            builder.Entity<PricingMaster>().ToTable(tableName);
            builder.Entity<PricingMaster>().HasKey(m => m.Id);
            builder.Entity<PricingMaster>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PricingMaster>().HasIndex(m => m.Created);
            builder.Entity<PricingMaster>().HasIndex(m => m.Modified);

            builder.Entity<PricingMaster>()
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
