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
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE_API.Tms.Models.Entities
{
    public class Schedule : CoreEntity
    {
        public Guid BookingId { get; set; }
        [MaxLength(50)]
        public string BookingNo { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid BookingContainerId { get; set; }
        public Guid BookingContainerDetailId { get; set; }
        [MaxLength(50)]
        public string ContainerNo { get; set; }
        public DateTime PickupPlan { get; set; }
        public DateTime? ActTd { get; set; }
        public DateTime DeliveryPlan { get; set; }
        [DefaultValue(null)]
        public DateTime? CompletedDate { get; set; }
        [DefaultValue(null)]
        public DateTime? InprocessDate { get; set; }
        [DefaultValue(null)]
        public DateTime? AssignedDate { get; set; }
        [DefaultValue(null)]
        public DateTime? RefuseDate { get; set; }
        [DefaultValue(null)]
        public DateTime? CancelDate { get; set; }
        public DateTime? ActTa { get; set; }
        public Guid ContainerTruckId { get; set; }
        [MaxLength(100)]
        public string ContainerTruckCode { get; set; }
        public Guid DriverId { get; set; }
        [MaxLength(100)]
        public string DriverName { get; set; }
        [DefaultValue(0)]
        public int HourNumberAlarm { get; set; }
        [DefaultValue(EScheduleStatus.ASSINGED)]
        public EScheduleStatus ScheduleStatus { get; set; }
        [MaxLength(20)]
        [DefaultValue("SGN")]
        public string StartLocation { get; set; }
        [MaxLength(255)]
        public string PickupAddress { get; set; }
        [MaxLength(20)]
        [DefaultValue("SGN")]
        public string EndLocation { get; set; }
        [MaxLength(255)]
        public string DeliveryAddress { get; set; }
        public decimal TransportCost { get; set; }
        public decimal Vgm { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
        [NotMapped]
        public virtual BookingContainer BookingContainer { get; set; }
        public virtual BookingContainerDetail BookingContainerDetail { get; set; }


        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "Schedule";

            //Generic
            builder.Entity<Schedule>().ToTable(tableName);
            builder.Entity<Schedule>().HasKey(m => m.Id);
            builder.Entity<Schedule>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Schedule>().HasIndex(m => m.Created);
            builder.Entity<Schedule>().HasIndex(m => m.Modified);
            builder.Entity<Schedule>().Property(e => e.TransportCost).HasPrecision(16, 2);
            builder.Entity<Schedule>().Property(e => e.Vgm).HasPrecision(16, 2);

            // QueryFilter for SoftDelete
            builder.Entity<Schedule>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationship
            builder.Entity<Schedule>().HasOne(m => m.BookingContainerDetail).WithOne(m => m.Schedule).HasForeignKey<Schedule>(m => m.BookingContainerDetailId);
        }

        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                //context.RemoveRangeAsync(UserRoles, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            //context.RemoveRange(UserRoles);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                //context.Entry(this)
                //    .Collection(m => m.UserRoles)
                //    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            //context.Entry(this)
            //    .Collection(m => m.UserRoles)
            //    .Load();
        }
    }
}
