using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Interop;

namespace CORE_API.CORE.Services.Abstract
{
    public interface IGenericEntityService<TEntity> where TEntity : CoreEntity
    {
        TEntity CreateProxy();


        #region Read / Find
        TEntity FindById(Guid id);

        TEntity FindOne(Expression<Func<TEntity, bool>> where = null);

        IQueryable<TEntity> FindQueryableList(int skip, int count, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool desc = true);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool desc = true);
        #endregion


        Task<SaveResponse<TEntity>> AddAsync(TEntity model);

        Task<SaveManyResponse<TEntity>> AddManyAsync(List<TEntity> modelList);

        Task<TEntity> DeleteById(Guid id);

        Task<List<TEntity>> DeleteMany(List<TEntity> modelList);

        Task<SaveResponse<TEntity>> UpdateAsync(TEntity model);

        Task<SaveManyResponse<TEntity>> UpdateManyAsync(List<TEntity> modelList);
        
        Task<int> Count(Expression<Func<TEntity, bool>> where = null);
    }
}