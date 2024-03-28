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
    public class Driver : CoreEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string DriverCode { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Tel { get; set; }
        public DateTime Birthday { get; set; }
        [DefaultValue("MALE")]
        [MaxLength(10)]
        public string Sex { get; set; }
        [MaxLength(50)]
        public string CardNo { get; set; }
        [MaxLength(50)]
        public string TaxCode { get; set; }
        [MaxLength(30)]
        public string CountryCode { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        public decimal BasicSalary { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User CreatedUser { get; set; }
        public Guid ModifiedBy { get; set; }
        public virtual User ModifiedUser { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "Driver";

            //Generic
            builder.Entity<Driver>().ToTable(tableName);
            builder.Entity<Driver>().HasKey(m => m.Id);
            builder.Entity<Driver>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Driver>().HasIndex(m => m.Created);
            builder.Entity<Driver>().HasIndex(m => m.Modified);
            builder.Entity<Driver>().Property(e => e.BasicSalary).HasPrecision(16, 2);

            // QueryFilter for SoftDelete
            builder.Entity<Driver>().HasQueryFilter(m => m.DeletedAt == null);

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
