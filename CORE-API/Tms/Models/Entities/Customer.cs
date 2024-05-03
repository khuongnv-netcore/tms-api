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
    public class Customer : CoreEntity
    {
        public string CustomerCode { get; set; }
        public string LegalName { get; set; }
        public string LanguageName { get; set; }
        public string LocationCode { get; set; }
        public string TaxCode { get; set; }
        [MaxLength(30)]
        public string Address { get; set; }
        public string CountryCode { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string SalesOffice { get; set; }
        public string SalesRepOffice {  get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            // Config
            string tableName = "Customer";

            // Generic
            builder.Entity<Customer>().ToTable(tableName);
            builder.Entity<Customer>().HasKey(m => m.Id);
            builder.Entity<Customer>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Customer>().HasIndex(m => m.Email).IsUnique(); // Unique email index
            builder.Entity<FixedAsset>().HasIndex(m => m.Created);
            builder.Entity<FixedAsset>().HasIndex(m => m.Modified);

            builder.Entity<Customer>()
                .HasQueryFilter(m => m.DeletedAt == null);

            // Relationship
            
        }
        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {

            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {

        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
        CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task>
            {

            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {

        }
    }
}
