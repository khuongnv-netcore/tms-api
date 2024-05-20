using System;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.Tms.Models.Entities
{
    public class BookingCharge : CoreEntity
    {      
        public Guid BookingId { get; set; }
        public Guid ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Vol { get; set; } = 0;
        public double Amount { get; set; } = 0;
        public Guid? PricingForCustomerDetailId { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "BookingCharge";

            //Generic
            builder.Entity<BookingCharge>().ToTable(tableName);
            builder.Entity<BookingCharge>().HasKey(m => m.Id);
            builder.Entity<BookingCharge>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<BookingCharge>().HasIndex(m => m.Created);
            builder.Entity<BookingCharge>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<BookingCharge>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationship
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
