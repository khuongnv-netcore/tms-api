using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using CORE_API.CORE.Repositories.Abstract;
using CORE_API.CORE.Models.Entities.Abstract;
using CORE_API.CORE.Contexts;
using EFCore.BulkExtensions;
using Stripe;

namespace CORE_API.CORE.Repositories
{
    public class GenericEntityRepository<TEntity> : IGenericEntityRepository<TEntity> where TEntity : CoreEntity
    {
        protected readonly CoreContext _context;

        private readonly Expression<Func<TEntity, object>> defaultSort = (m => m.Created);

        private readonly Expression<Func<TEntity, bool>> defaultWhereAll = (m => true);

        public GenericEntityRepository(CoreContext context)
        {
            _context = context;
        }

        public TEntity CreateProxy()
        {
            return _context.CreateProxy<TEntity>();
        }

        public async Task AddAsync(TEntity model)
        {
            await _context.Set<TEntity>().AddAsync(model);
        }

        public IQueryable<TEntity> Find(int skip = -1, int count = -1, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool desc = true)
        {

            if(where == null)
            {
                where = defaultWhereAll;
            }
            if(order == null)
            {
                order = defaultSort;
            }

            if (skip != -1 && count != -1)
            {
                if (desc)
                {
                    return _context.Set<TEntity>().Where(where).OrderByDescending(order).Skip(skip).Take(count);
                }
                else
                {
                    return _context.Set<TEntity>().Where(where).OrderBy(order).Skip(skip).Take(count);
                }
            }
            if (desc)
            {
                return _context.Set<TEntity>().Where(where).OrderByDescending(order);
            }
            else
            {
                return _context.Set<TEntity>().Where(where).OrderBy(order);
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null)
        {
            if(null == where)
            {
                return await _context.Set<TEntity>().CountAsync();
            }
            return await _context.Set<TEntity>().Where(where).CountAsync();
        }

        public void Update(TEntity model)
        {
            _context.Set<TEntity>().Update(model);
        }

        public TEntity Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void BulkInsertOrUpdate(List<TEntity> entities)
        {
            // Bulk insert or update the list of entities
            _context.BulkInsertOrUpdate(entities);
        }
    }
}
