using CORE_API.CORE.Models.Views;
using CORE_API.Tms.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Views
{
    public class ScheduleInputResource : CoreInputResource
    {
        public Guid BookingId { get; set; }
        [MaxLength(50)]
        public string BookingNo { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid BookingContainerId { get; set; }
        public Guid? BookingContainerDetailId { get; set; }
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
        public Guid ModifiedBy { get; set; }
    }

    public class ScheduleOutputResource : CoreOutputResource
    {
        public Guid BookingId { get; set; }
        [MaxLength(50)]
        public string BookingNo { get; set; }
        public Guid? ContainerId { get; set; }
        public string ContainerCode { get; set; }
        public Guid BookingContainerId { get; set; }
        public Guid? BookingContainerDetailId { get; set; }
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
        public Guid ModifiedBy { get; set; }
    }

    public class ScheduleBookingOutputResource : CoreOutputResource
    {
        public Guid BookingId { get; set; }
        [MaxLength(50)]
        public string BookingNo { get; set; }
        public Guid? ContainerId { get; set; }
        public string ContainerCode { get; set; }
        public Guid BookingContainerId { get; set; }
        public Guid? BookingContainerDetailId { get; set; }
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
        public Guid ModifiedBy { get; set; }
    }

    public class ScheduleForBookingOutputResource
    {
        public Guid BookingId { get; set; }
        public string BookingNo { get; set; }
        public List<ScheduleBookingOutputResource> schedules { get; set; }
    }

    public class CreateOrUpdateScheduleInputResource
    {
        public Guid Id { get; set; } 
        public Guid BookingId { get; set; }
        [MaxLength(50)]
        public string BookingNo { get; set; }
        public Guid? ContainerId { get; set; }
        public Guid BookingContainerId { get; set; }
        public Guid? BookingContainerDetailId { get; set; }
        [MaxLength(50)]
        public string ContainerNo { get; set; }
        public DateTime PickupPlan { get; set; }
        public DateTime? ActTd { get; set; }
        public DateTime DeliveryPlan { get; set; }
        //[DefaultValue(null)]
        //public DateTime? CompletedDate { get; set; }
        //[DefaultValue(null)]
        //public DateTime? InprocessDate { get; set; }
        //[DefaultValue(null)]
        //public DateTime? AssignedDate { get; set; }
        //[DefaultValue(null)]
        //public DateTime? RefuseDate { get; set; }
        //[DefaultValue(null)]
        //public DateTime? CancelDate { get; set; }
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
        //public Guid CreatedBy { get; set; }
        //public Guid ModifiedBy { get; set; }
    }

    public class CreateOrUpdateScheduleOutputResource : CoreOutputResource
    {
        public Guid BookingId { get; set; }
        [MaxLength(50)]
        public string BookingNo { get; set; }
        public Guid? ContainerId { get; set; }
        public string ContainerCode { get; set; }
        public Guid BookingContainerId { get; set; }
        public Guid? BookingContainerDetailId { get; set; }
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
        public Guid ModifiedBy { get; set; }
    }
}
