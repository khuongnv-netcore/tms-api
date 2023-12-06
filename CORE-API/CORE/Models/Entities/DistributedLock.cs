using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CORE_API.CORE.Models.Entities
{
    public class DistributedLock : CoreEntity
    {
        public string LockName { get; set; }

        public DateTime Expiration { get; set; }

        public bool IsExpired => Expiration > DateTime.UtcNow;

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "DistributedLock";

            //Generic
            builder.Entity<DistributedLock>().ToTable(tableName);
            builder.Entity<DistributedLock>().HasKey(m => m.Id);
            builder.Entity<DistributedLock>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<DistributedLock>().HasIndex(m => m.LockName).IsUnique();

            //Relationship
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
