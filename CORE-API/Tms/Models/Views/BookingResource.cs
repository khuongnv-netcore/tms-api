using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CORE_API.Tms.Models.Entities;

namespace CORE_API.Tms.Models.Views
{
    public class BookingInputResource : CoreInputResource
    {
        public string BookingNo { get; set; }
        public EBookingType BookingType { get; set; } = EBookingType.EXPORT;
        public Guid ShipperId { get; set; }
        public Guid ForwarderId { get; set; }
        public Guid ConsigneeId { get; set; }
        public Guid VirtualBookingId { get; set; }
        public Guid RequestOrderId { get; set; }
        public string VirtualBookingNo { get; set; }
        public string RequestOrderNo { get; set; }
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
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid ApprovedBy { get; set; }
        public List<BookingContainerInputResource> BookingContainers { get; set; }
    }

    public class BookingOutputResource : CoreOutputResource
    {
        public string BookingNo { get; set; }
        public EBookingType BookingType { get; set; } = EBookingType.EXPORT;
        public Guid ShipperId { get; set; }
        public Guid ForwarderId { get; set; }
        public Guid ConsigneeId { get; set; }
        public Guid VirtualBookingId { get; set; }
        public Guid RequestOrderId { get; set; }
        public string VirtualBookingNo { get; set; }
        public string RequestOrderNo { get; set; }
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
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid ApprovedBy { get; set; }
        public List<BookingContainerOutputResource> BookingContainers { get; set; }
    }

    public class BookingOutputResourceEx
    {
        public string Id { get; set; }
        public string BookingNo { get; set; }
    }
}
