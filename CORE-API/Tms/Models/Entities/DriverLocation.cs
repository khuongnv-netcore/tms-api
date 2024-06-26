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
    public class DriverLocation : CoreEntity
    {
        public EOperationType OperationType { get; set; }
        public Guid? ScheduleId { get; set; }
        public Guid? DriverId { get; set; }
        public Guid? UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; }
        public DateTime GpsDate { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "DriverLocation";

            // Generic
            builder.Entity<DriverLocation>().ToTable(tableName);
            builder.Entity<DriverLocation>().HasKey(m => m.Id);
            builder.Entity<DriverLocation>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<DriverLocation>().HasIndex(m => m.Created);
            builder.Entity<DriverLocation>().HasIndex(m => m.Modified);

            builder.Entity<Location>()
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
