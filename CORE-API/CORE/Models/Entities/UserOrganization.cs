using System;
using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using CORE_API.CORE.Models.Enums;

namespace CORE_API.CORE.Models.Entities
{
    public class UserOrganization : CoreEntity
    {
        public virtual User User { get; set; }
        public Guid UserId { get; set; }

        public virtual Organization Organization { get; set; }
        public Guid OrganizationId { get; set; }

        public EOrganizationUserRole OrganizationUserRole { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "UserOrganization";

            //Generic
            builder.Entity<UserOrganization>().ToTable(tableName);
            builder.Entity<UserOrganization>().HasKey(m => m.Id);
            builder.Entity<UserOrganization>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserOrganization>().HasIndex(m => m.Created);
            builder.Entity<UserOrganization>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<UserOrganization>().HasQueryFilter(m => m.DeletedAt == null);
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
