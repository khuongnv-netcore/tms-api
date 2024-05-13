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
    public class Container : CoreEntity
    {
        [MaxLength(30)]
        public string ContainerCode { get; set; }
        [MaxLength(255)]
        public string ContainerSize { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "Container";

            // Generic
            builder.Entity<Container>().ToTable(tableName);
            builder.Entity<Container>().HasKey(m => m.Id);
            builder.Entity<Container>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Container>().HasIndex(m => m.Created);
            builder.Entity<Container>().HasIndex(m => m.Modified);

            builder.Entity<Container>()
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
