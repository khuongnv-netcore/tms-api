using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.Tms.Models.Entities
{
    public class PricingForCustomer : CoreEntity
    {
        public DateTime FromDatePeriod { get; set; }
        public DateTime ToDatePeriod { get; set; }
        public Guid CustomerId {  get; set; }
        public Guid SellerId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        public virtual IList<PricingForCustomerDetail> PricingForCustomerDetails { get; set; }

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
                context.RemoveRangeAsync(PricingForCustomerDetails, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(PricingForCustomerDetails);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.Entry(this)
                    .Collection(m => m.PricingForCustomerDetails)
                    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(m => m.PricingForCustomerDetails)
                .Load();
        }
    }
}
