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
    public class Booking : CoreEntity
    {        
        public string BookingNo { get; set; }
        public EBookingType BookingType { get; set; } = EBookingType.EXPORT;
        public Guid ShipperId { get; set; }
        public Guid ForwarderId { get; set; }
        public Guid ConsigneeId { get; set; }
        public Guid VirtualBookingId { get; set; }
        public Guid RequestOrderId { get; set; }
        public string VirtualBookingNo { get; set; } = null!;
        public string RequestOrderNo { get; set; } = null!;
        public EScheduleStatusOfBooking ScheduleStatus { get; set; } = EScheduleStatusOfBooking.EMPTY;
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }

        #region Thông tin tàu
        public string TVVD { get; set; }
        public string POR1 { get; set; }
        public string POR2 { get; set; }
        public string POL1 { get; set; }
        public string POL2 { get; set; }
        public string POD1 { get; set; }
        public string POD2 { get; set; }
        public string DEL1 { get; set; }
        public string DEL2 { get; set; }
        public string RDTerm1 { get; set; }
        public string RDTerm2 { get; set; }
        public string BLNo { get; set; }
        public bool SI { get; set; } = false;
        public bool BRD { get; set; } = false;

        #endregion

        #region Thông tin vận chuyển hàng hoá xuất nhập
        public string PickUpCy { get; set; }
        public string FullReturnCy { get; set; }
        public string BKGContactName { get; set; }
        public string BKGContactEmail { get; set; }
        public string BKGContactTel { get; set; }
        #endregion

        #region Thông tin hàng hoá và loại Container
        public bool Fh { get; set; } = false;
        public string CMTD1 { get; set; }
        public string CMTD2 { get; set; }
        public double Weight { get; set; } = 0;
        public EUnitType UnitType { get; set; } = EUnitType.KG;
        public string LOFC1 { get; set; }
        public string LOFC2 { get; set; }
        public string ExtRemark { get; set; }
        public string IntRemark { get; set; }

        #endregion 

        public DateTime ClosingTime { get; set; } = DateTime.Now;
        public DateTime SaillingDueDate { get; set; } = DateTime.Now;
        public DateTime PickUpDT { get; set; } = DateTime.Now;
        public DateTime ETBDT { get; set; } = DateTime.Now;
        public EBookingStatus Status { get; set; } = EBookingStatus.ORDER;
        public Guid? ContractId { get; set; } // Pricing For Customer
        public Guid? SellerId { get; set; } // Employee
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
        public Guid ApprovedBy { get; set; } // Employee
        public virtual Employee ApprovedUser { get; set; }
        public virtual IList<BookingContainer> BookingContainers { get; set; }
        public virtual IList<BookingCharge> BookingCharges { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "Booking";

            //Generic
            builder.Entity<Booking>().ToTable(tableName);
            builder.Entity<Booking>().HasKey(m => m.Id);
            builder.Entity<Booking>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Booking>().HasIndex(m => m.Created);
            builder.Entity<Booking>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<Booking>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationship
        }

        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.RemoveRangeAsync(BookingContainers, cancellationToken),
                context.RemoveRangeAsync(BookingCharges, cancellationToken),
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(BookingContainers);
            context.RemoveRange(BookingCharges);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {
                context.Entry(this)
                    .Collection(m => m.BookingContainers)
                    .LoadAsync(cancellationToken),
                context.Entry(this)
                    .Collection(m => m.BookingCharges)
                    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }
        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(m => m.BookingContainers)
                .Load();
            context.Entry(this)
                .Collection(m => m.BookingCharges)
                .Load();
        }
    }
}
