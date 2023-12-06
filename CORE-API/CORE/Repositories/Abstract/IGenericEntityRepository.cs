using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CORE_API.CORE.Models.Entities.Abstract;

namespace CORE_API.CORE.Repositories.Abstract
{
    public interface IGenericEntityRepository<TEntity> where TEntity : CoreEntity
    {
        TEntity CreateProxy();

        Task AddAsync(TEntity model);

        IQueryable<TEntity> Find(int skip = -1, int count = -1, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool desc = true);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null);

        void Update(TEntity model);

        TEntity Delete(TEntity entity);

        Task SaveChangesAsync();
    }
}