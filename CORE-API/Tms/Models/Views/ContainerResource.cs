using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Views
{
    public class ContainerInputResource : CoreInputResource
    {
        [MaxLength(30)]
        public string ContainerCode { get; set; }
        [MaxLength(255)]
        public string ContainerSize { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }

    public class ContainerOutputResource : CoreOutputResource
    {
        [MaxLength(30)]
        public string ContainerCode { get; set; }
        [MaxLength(255)]
        public string ContainerSize { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
