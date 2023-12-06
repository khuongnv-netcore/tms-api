using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORE_API.CORE.Models.Entities
{
    public class Organization : CoreEntity
    {
        public string Name { get; set; }
        public virtual IList<UserOrganization> UserOrganizations { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "Organization";

            //Generic
            builder.Entity<Organization>().ToTable(tableName);
            builder.Entity<Organization>().HasKey(m => m.Id);
            builder.Entity<Organization>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();

            //Entity Specific

            // QueryFilter for SoftDelete
            builder.Entity<Organization>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationships
            builder.Entity<Organization>().HasMany(m => m.UserOrganizations).WithOne(m => m.Organization).HasForeignKey(m => m.OrganizationId);
        }

        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                context.RemoveRangeAsync(UserOrganizations, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(UserOrganizations);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                context.Entry(this)
                    .Collection(org => org.UserOrganizations)
                    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(org => org.UserOrganizations)
                .Load();
        }
    }
}
