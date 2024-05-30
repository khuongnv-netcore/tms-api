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
    public class FixedAsset : CoreEntity
    {
        [MaxLength(200)] 
        public string DriverName { get; set; }
        public string DriverId { get; set; }
        [MaxLength(50)]
        public string DriverCode { get; set; }
        [MaxLength(300)]
        public string Manuafacture { get; set; }
        [MaxLength(50)]
        public string FixedAssetCode { get; set; }
        [MaxLength(255)]
        public string Desc { get; set; }
        public EFixedAssetType FixedAssetType { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "FixedAsset";

            //Generic
            builder.Entity<FixedAsset>().ToTable(tableName);
            builder.Entity<FixedAsset>().HasKey(m => m.Id);
            builder.Entity<FixedAsset>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<FixedAsset>().HasIndex(m => m.Created);
            builder.Entity<FixedAsset>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<FixedAsset>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationship
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
