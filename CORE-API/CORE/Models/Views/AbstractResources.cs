using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Models.Views
{
    public class CoreInputResource
    {
    }

    public class CoreUpdateResource<TEntity> where TEntity : CoreInputResource
    {
        public Guid Id {get; set;}
        public TEntity Resource {get; set;}
    }

    public class CoreOutputResource
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class CoreListOutputResource<TEntity> where TEntity : CoreOutputResource
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public int TotalEntities { get; set; }
    }

    public class CoreListOutputResourceEx<TEntity>
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public int TotalEntities { get; set; }
    }

    public class CoreScheduleListOutputResource<TEntity>
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public int TotalEntities { get; set; }
    }


}
