using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Entities
{
    public class Role : CoreEntity
    {
        public string DisplayName { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "Role";

            //Generic
            builder.Entity<Role>().ToTable(tableName);
            builder.Entity<Role>().HasKey(m => m.Id);
            builder.Entity<Role>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().HasIndex(m => m.Created);
            builder.Entity<Role>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<Role>().HasQueryFilter(m => m.DeletedAt == null);

            //Relationship
            builder.Entity<Role>().HasMany(m => m.UserRoles).WithOne(m => m.Role).HasForeignKey(m => m.RoleId);
        }

        public override async Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                context.RemoveRangeAsync(UserRoles, cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context)
        {
            context.RemoveRange(UserRoles);
        }

        public override async Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            var taskList = new List<Task> {
                context.Entry(this)
                    .Collection(m => m.UserRoles)
                    .LoadAsync(cancellationToken)
            };

            await Task.WhenAll(taskList);
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context)
        {
            context.Entry(this)
                .Collection(m => m.UserRoles)
                .Load();
        }
    }
}
