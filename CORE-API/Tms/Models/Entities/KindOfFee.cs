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
    public class KindOfFee : CoreEntity
    {
        public string FeeName { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "KindOfFee";

            // Generic
            builder.Entity<KindOfFee>().ToTable(tableName);
            builder.Entity<KindOfFee>().HasKey(m => m.Id);
            builder.Entity<KindOfFee>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<KindOfFee>().HasIndex(m => m.FeeName).IsUnique(true);
            builder.Entity<KindOfFee>().HasIndex(m => m.Created);
            builder.Entity<KindOfFee>().HasIndex(m => m.Modified);

            builder.Entity<KindOfFee>()
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
