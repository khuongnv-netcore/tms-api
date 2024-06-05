using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CORE_API.Tms.Models.Entities
{
    public class PricingMasterDetail : CoreEntity
    {
        public Guid PricingMasterId {  get; set; }
        public decimal UnitPrice { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid? FromLocationId { get; set; }
        public Guid? ToLocationId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        public virtual Location FromLocation { get; set; }
        public virtual Location ToLocation { get; set; }
        public virtual Container Container { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "PricingMasterDetail";

            // Generic
            builder.Entity<PricingMasterDetail>().ToTable(tableName);
            builder.Entity<PricingMasterDetail>().HasKey(m => m.Id);
            builder.Entity<PricingMasterDetail>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PricingMasterDetail>().HasIndex(m => m.Created);
            builder.Entity<PricingMasterDetail>().HasIndex(m => m.Modified);

            builder.Entity<PricingMasterDetail>()
                .HasQueryFilter(m => m.DeletedAt == null);

            // Relationship
        }
        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {

        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {

        }
    }
}
