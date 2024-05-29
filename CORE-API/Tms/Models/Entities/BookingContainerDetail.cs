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

namespace CORE_API.Tms.Models.Entities
{
    public class BookingContainerDetail : CoreEntity
    {       
        public Guid BookingId { get; set; }
        public string BookingNo { get; set; }
        public Guid ContainerId { get; set; }
        public string ContainerNo { get; set; }
        public Guid BookingContainerId { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public int Package { get; set; } = 0;
        public double Weight { get; set; } = 0;
        public double VGM { get; set; } = 0;
        public double Measure { get; set; } = 0;
        public string ST { get; set; }
        public bool RF { get; set; } = false;
        public bool Scheduled { get; set; } = false;
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
        public Guid? ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual BookingContainer BookingContainer { get; set; }

        //
        //[ForeignKey("BookingId")]
        //public Booking? Booking { get; set; }

        //[ForeignKey("BookingContainerId")]
        //public BookingContainer? BookingContainer { get; set; }

        //[ForeignKey("ContainerId")]
        //public Container? Container { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "BookingContainerDetail";

            //Generic
            builder.Entity<BookingContainerDetail>().ToTable(tableName);
            builder.Entity<BookingContainerDetail>().HasKey(m => m.Id);
            builder.Entity<BookingContainerDetail>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<BookingContainerDetail>().HasIndex(m => m.Created);
            builder.Entity<BookingContainerDetail>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<BookingContainerDetail>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationship
        }

        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.RemoveAsync(Schedule, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.Remove(Schedule);
        }

        public override Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
        public override void LoadRelations(SoftDeletes.Core.DbContext context){}
    }
}
