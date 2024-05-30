using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Models.Interop;
using CORE_API.CORE.Repositories.Abstract;
using CORE_API.CORE.Services.Abstract;

namespace CORE_API.CORE.Services
{
    public class GenericEntityService<TEntity> : IGenericEntityService<TEntity> where TEntity : CoreEntity
    {

        private readonly IGenericEntityRepository<TEntity> _repository;

        public GenericEntityService(IGenericEntityRepository<TEntity> baseRepository)
        {
            _repository = baseRepository;
        }

        public TEntity CreateProxy()
        {
            return _repository.CreateProxy();
        }

        #region Read / Find
        public TEntity FindById(Guid id)
        {
            return FindOne(m=>m.Id.Equals(id));
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> where = null)
        {
            var result = _repository.Find(0, 1, where);

            return result.FirstOrDefault();
        }
        
        public IQueryable<TEntity> FindQueryableList(int skip, int count, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool desc = true)
        {
            return _repository.Find(skip, count, where, order, desc);
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool desc = true)
        {
            return _repository.Find(-1, -1, where, order, desc);
        }

        #endregion

        public async Task<SaveResponse<TEntity>> AddAsync(TEntity model)
        {
            try
            {
                await _repository.AddAsync(model);
                await _repository.SaveChangesAsync();

                return new SaveResponse<TEntity>(model);
            }
            catch (Exception ex)
            {
                return new SaveResponse<TEntity>($"An error occured when saving the entity: {ex.Message}", ex);
            }

        }

        public async Task<SaveManyResponse<TEntity>> AddManyAsync(List<TEntity> modelList)
        {
            var savedList = new List<TEntity>();
            try
            {
                foreach(TEntity model in modelList)
                {
                    await _repository.AddAsync(model);
                    savedList.Add(model);
                }
                await _repository.SaveChangesAsync();

                return new SaveManyResponse<TEntity>(savedList);
            }catch(Exception ex)
            {
                //Do some logging
                return new SaveManyResponse<TEntity>($"An error occured when saving the entity: {ex.Message}", ex);
            }

        }

        public async Task<TEntity> DeleteById(Guid id)
        {
            var entity = FindById(id);
            var deleted = _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return deleted;
        }

        public async Task<List<TEntity>> DeleteMany(List<TEntity> modelList)
        {
            var deleteList = new List<TEntity>();
            foreach (TEntity model in modelList)
            {
                _repository.Delete(model);
                deleteList.Add(model);
            }
            await _repository.SaveChangesAsync();

            return deleteList;
            
        }

        public async Task<SaveResponse<TEntity>> UpdateAsync(TEntity model)
        {
            try
            {
                _repository.Update(model);
                await _repository.SaveChangesAsync();

                return new SaveResponse<TEntity>(model);
            }
            catch (Exception ex)
            {
                //Do some logging
                return new SaveResponse<TEntity>($"An error occured when saving the symptom: {ex.Message}", ex);
            }
        }

        public async Task<SaveManyResponse<TEntity>> UpdateManyAsync(List<TEntity> modelList)
        {
            var updatedList = new List<TEntity>();
            try
            {
                foreach (TEntity model in modelList)
                {
                    _repository.Update(model);
                    updatedList.Add(model);
                }
                await _repository.SaveChangesAsync();

                return new SaveManyResponse<TEntity>(updatedList);
            }
            catch (Exception ex)
            {
                //Do some logging
                return new SaveManyResponse<TEntity>($"An error occured when saving the entity: {ex.Message}", ex);
            }

        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> where = null)
        {
            return await _repository.CountAsync(where);
        }

        public async Task<SaveManyResponse<TEntity>> BulkInsertOrUpdate(List<TEntity> modelList)
        {
            try
            {
                _repository.BulkInsertOrUpdate(modelList);
                await _repository.SaveChangesAsync();

                return new SaveManyResponse<TEntity>(modelList);
            }
            catch (Exception ex)
            {
                //Do some logging
                return new SaveManyResponse<TEntity>($"An error occured when saving the entity: {ex.Message}", ex);
            }

        }
    }
}
