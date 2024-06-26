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
    public class AdvanceMoney : CoreEntity
    {
        public Guid? BookingId { get; set; }
        public Guid? EmployeeId { get; set; }
        public decimal Money { get; set; }
        public string Note { get; set; }
        public DateTime? AdvanceMoneyDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual IList<AdvanceMoneyDocument> AdvanceMoneyDocuments { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "AdvanceMoney";

            // Generic
            builder.Entity<AdvanceMoney>().ToTable(tableName);
            builder.Entity<AdvanceMoney>().HasKey(m => m.Id);
            builder.Entity<AdvanceMoney>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<AdvanceMoney>().HasIndex(m => m.Created);
            builder.Entity<AdvanceMoney>().HasIndex(m => m.Modified);

            builder.Entity<AdvanceMoney>()
                .HasQueryFilter(m => m.DeletedAt == null);

            // Relationship
            builder.Entity<AdvanceMoney>()
                .HasMany(e => e.AdvanceMoneyDocuments)
                .WithOne(e => e.AdvanceMoney)
                .HasForeignKey(e => e.AdvanceMoneyId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.RemoveRangeAsync(AdvanceMoneyDocuments, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(AdvanceMoneyDocuments);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.Entry(this)
                    .Collection(m => m.AdvanceMoneyDocuments)
                    .LoadAsync()
            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(m => m.AdvanceMoneyDocuments)
                .Load();
        }
    }
}
