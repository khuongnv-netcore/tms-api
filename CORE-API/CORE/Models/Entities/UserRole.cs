using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace CORE_API.CORE.Models.Entities
{
    public class UserRole : CoreEntity
    {
        public virtual User User { get; set; }
        public Guid UserId { get; set; }

        public virtual Role Role { get; set; }
        public Guid RoleId { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "UserRole";

            //Generic
            builder.Entity<UserRole>().ToTable(tableName);
            builder.Entity<UserRole>().HasKey(m => m.Id);
            builder.Entity<UserRole>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserRole>().HasIndex(m => m.Created);
            builder.Entity<UserRole>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<UserRole>().HasQueryFilter(m => m.DeletedAt == null);
        }

        public override Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context){}

        public override Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context){}
    }
}
