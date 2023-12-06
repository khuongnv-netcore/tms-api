using System;
using System.ComponentModel.DataAnnotations.Schema;
using SoftDeletes.Core;
using SoftDeletes.ModelTools;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Entities.Abstract
{
    public abstract class CoreEntity : IComparable<CoreEntity>, IEquatable<CoreEntity>, ISoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
        
        public DateTime Modified { get; set; }

        public DateTime? DeletedAt { get; set; }

        bool ISoftDelete.ForceDelete { get; set; }

        public int CompareTo(CoreEntity other)
        {
            if (Id.Equals(other.Id))
            {
                return 0;
            }

            return Created.CompareTo(other.Created);
        }

        public bool Equals(CoreEntity other)
        {
            return Id.Equals(other.Id);
        }

        public abstract Task OnSoftDeleteAsync(DbContext context, CancellationToken cancellationToken = default(CancellationToken));

        public abstract void OnSoftDelete(DbContext context);

        public abstract Task LoadRelationsAsync(DbContext context, CancellationToken cancellationToken = default(CancellationToken));

        public abstract void LoadRelations(DbContext context);
    }
}
