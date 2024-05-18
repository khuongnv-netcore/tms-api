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
    public class BookingContainerInputResource : CoreInputResource
    {
        public Guid BookingId { get; set; }
        public Guid ContainerId { get; set; }
        public string ContainerCode { get; set; }
        public int Vol { get; set; } = 0;
        public double EQSub { get; set; } = 0;
        public double SOC { get; set; } = 0;
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
        public List<BookingContainerDetailInputResource> bookingContainerDetails { get; set; }
    }

    public class BookingContainerOutputResource : CoreOutputResource
    {
        public Guid BookingId { get; set; }
        public Guid ContainerId { get; set; }
        public string ContainerCode { get; set; }
        public int Vol { get; set; } = 0;
        public double EQSub { get; set; } = 0;
        public double SOC { get; set; } = 0;
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }
        public List<BookingContainerDetailOutputResource> bookingContainerDetails { get; set; }
    }
}
