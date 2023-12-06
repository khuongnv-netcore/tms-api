using CORE_API.CORE.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Entities
{
    public class AuditLog : CoreEntity
    {
        public Guid? UserId { get; set; }

        public Guid TransactionId { get; set; }

        public string TableName { get; set; }

        public EntityState Action { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public virtual User User { get; set; }


        [NotMapped]
        public Dictionary<string, object> KeyValuesDict { get; } = new Dictionary<string, object>();

        [NotMapped]
        public Dictionary<string, object> OldValuesDict { get; } = new Dictionary<string, object>();

        [NotMapped]
        public Dictionary<string, object> NewValuesDict { get; } = new Dictionary<string, object>();

        [NotMapped]
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        [NotMapped]
        public bool HasTemporaryProperties => TemporaryProperties.Any();


        public void ProcessDictionaries()
        {
            KeyValues = System.Text.Json.JsonSerializer.Serialize(KeyValuesDict);
            OldValues = OldValuesDict.Count == 0 ? null : System.Text.Json.JsonSerializer.Serialize(OldValuesDict);
            NewValues = NewValuesDict.Count == 0 ? null : System.Text.Json.JsonSerializer.Serialize(NewValuesDict);
        }

        internal static void OnModelCreating(ModelBuilder builder)
        {
            //Config
            string tableName = "AuditLog";

            //Generic
            builder.Entity<AuditLog>().ToTable(tableName);
            builder.Entity<AuditLog>().HasKey(m => m.Id);
            builder.Entity<AuditLog>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<AuditLog>().HasIndex(m => m.TableName);
            builder.Entity<AuditLog>().HasIndex(m => m.Created);
            builder.Entity<AuditLog>().HasIndex(m => m.Modified);

            //Entity Specific

            //Relationships
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
