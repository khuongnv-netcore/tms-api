using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using System;
using CORE_API.Tms.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CORE_API.Tms.Models.Views
{
    public class FixedAssetInputResource : CoreInputResource
    {
        [MaxLength(300)]
        public string Manuafacture { get; set; }
        [MaxLength(50)]
        public string FixedAssetCode { get; set; }
        [MaxLength(255)]
        public string Desc { get; set; }
        public EFixedAssetType FixedAssetType { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }

    public class FixedAssetOutputResource : CoreOutputResource
    {
        [MaxLength(300)]
        public string Manuafacture { get; set; }
        [MaxLength(50)]
        public string FixedAssetCode { get; set; }
        [MaxLength(255)]
        public string Desc { get; set; }
        public EFixedAssetType FixedAssetType { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
