using System;
using Microsoft.EntityFrameworkCore;
using CORE_API.CORE.Models.Entities.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Entities
{
    public class Authentication : CoreEntity
    {
        public string Token { get; set; }
        public virtual User User { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "Authentication";

            //Generic
            builder.Entity<Authentication>().ToTable(tableName);
            builder.Entity<Authentication>().HasKey(m => m.Id);
            builder.Entity<Authentication>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Authentication>().HasIndex(m => m.Created);
            builder.Entity<Authentication>().HasIndex(m => m.Modified);

            // QueryFilter for SoftDelete
            builder.Entity<Authentication>().HasQueryFilter(m => m.DeletedAt == null);
        }

        public override Task OnSoftDeleteAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public override void OnSoftDelete(SoftDeletes.Core.DbContext context) { }

        public override Task LoadRelationsAsync(SoftDeletes.Core.DbContext context,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public override void LoadRelations(SoftDeletes.Core.DbContext context) { }
    }
}
