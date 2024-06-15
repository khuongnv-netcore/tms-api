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
    public class AutoNumber : CoreEntity
    {
        public EAutoNumberType AutoNumberType { get; set; }
        public string Prefix { get; set; }
        public string RegExp { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int CurrentNumber { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "AutoNumber";

            // Generic
            builder.Entity<AutoNumber>().ToTable(tableName);
            builder.Entity<AutoNumber>().HasKey(m => m.Id);
            builder.Entity<AutoNumber>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<AutoNumber>().HasIndex(m => m.Created);
            builder.Entity<AutoNumber>().HasIndex(m => m.Modified);
            builder.Entity<AutoNumber>().HasIndex(m => m.AutoNumberType).IsUnique(true);

            builder.Entity<AutoNumber>()
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
