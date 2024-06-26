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
    public class Notification : CoreEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Open { get; set; }
        public Guid UserId { get; set; }
        public ENotificationType NotificationType { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "Notification";

            // Generic
            builder.Entity<Notification>().ToTable(tableName);
            builder.Entity<Notification>().HasKey(m => m.Id);
            builder.Entity<Notification>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Notification>().HasIndex(m => m.Created);
            builder.Entity<Notification>().HasIndex(m => m.Modified);

            builder.Entity<Notification>()
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
