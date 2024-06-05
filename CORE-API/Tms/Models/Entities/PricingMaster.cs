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
        public EFeeType FeeType { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        public virtual IList<PricingMasterDetail> PricingMasterDetails { get; set; }

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
                context.RemoveRangeAsync(PricingMasterDetails, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(PricingMasterDetails);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.Entry(this)
                    .Collection(m => m.PricingMasterDetails)
                    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(m => m.PricingMasterDetails)
                .Load();
        }
    }
}
