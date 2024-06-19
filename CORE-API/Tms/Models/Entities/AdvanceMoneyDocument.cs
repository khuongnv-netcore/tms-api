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
using NetTopologySuite.Index.HPRtree;

namespace CORE_API.Tms.Models.Entities
{
    public class AdvanceMoneyDocument : CoreEntity
    {
        public Guid AdvanceMoneyId { get; set; }
        public decimal Money { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFilePath { get; set; }
        public bool Internal { set; get; } = false;
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        public virtual BookingCharge? BookingCharge { get; set; }
        public virtual AdvanceMoney AdvanceMoney { get; set; }
        

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
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(false);
        }
        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.RemoveAsync(BookingCharge, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            if (BookingCharge != null) {
                context.Remove(BookingCharge);
            }
            
        }

        public override Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
        }

        public void OnAddItem(SoftDeletes.Core.DbContext context) {
            if(AdvanceMoney.BookingId != null)
            {
                var bookingCharge = new BookingCharge
                {
                    BookingId = AdvanceMoney.BookingId??Guid.Empty,
                    AdvanceMoneyDocumentId = Id,
                    UnitPrice = Money,
                    Vol = 1,
                    Amount = Money
                };
                context.AddAsync(bookingCharge);
            }
        }
    }
}
