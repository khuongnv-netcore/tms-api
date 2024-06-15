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
    public class AdvanceMoneyDocument : CoreEntity
    {
        public Guid AdvanceMoneyId { get; set; }
        public decimal Money { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFilePath { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        public virtual BookingCharge? BookingCharge { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "AdvanceMoneyDocument";

            // Generic
            builder.Entity<AdvanceMoneyDocument>().ToTable(tableName);
            builder.Entity<AdvanceMoneyDocument>().HasKey(m => m.Id);
            builder.Entity<AdvanceMoneyDocument>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<AdvanceMoneyDocument>().HasIndex(m => m.Created);
            builder.Entity<AdvanceMoneyDocument>().HasIndex(m => m.Modified);

            builder.Entity<AdvanceMoneyDocument>()
                .HasQueryFilter(m => m.DeletedAt == null);

            // Relationship
            builder.Entity<AdvanceMoneyDocument>()
            .HasOne(e => e.BookingCharge)
            .WithOne(e => e.AdvanceMoneyDocument)
            .HasForeignKey<BookingCharge>(e => e.AdvanceMoneyDocumentId)
            .IsRequired(false);
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
