using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.Tms.Models.Entities
{
    public class PricingForCustomerDetail : CoreEntity
    {
        public Guid PricingMasterId {  get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PriceForSale { get; set; }
        public Guid PricingForCustomerId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "PricingForCustomerDetail";

            // Generic
            builder.Entity<PricingForCustomerDetail>().ToTable(tableName);
            builder.Entity<PricingForCustomerDetail>().HasKey(m => m.Id);
            builder.Entity<PricingForCustomerDetail>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PricingForCustomerDetail>().HasIndex(m => m.Created);
            builder.Entity<PricingForCustomerDetail>().HasIndex(m => m.Modified);

            builder.Entity<PricingForCustomerDetail>()
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
