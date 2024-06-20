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
    public class BookingContainerDetailInputResource : CoreInputResource
    {
        public Guid Id { get; set; }
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
        public Guid ModifiedBy { get; set; }
    }

    public class BookingContainerDetailOutputResource : CoreOutputResource
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
        public Guid ModifiedBy { get; set; }
    }
}
