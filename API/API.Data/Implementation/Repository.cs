using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Data.Extensions;
using API.Data.Interfaces;
using API.Domain.Entities.Interfaces;

namespace API.Data.Implementation
{
    public abstract class Repository<T> : IRepository<T>
        where T : class, IBaseEntity
    {
        private readonly DbContext _databaseContext;

        protected DbSet<T> DbSet => _databaseContext.Set<T>();

        protected Repository(DbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public virtual async Task<T> Add(T entity)
        {
            var result = await DbSet.AddAsync(entity);

            return result.Entity;
        }

        protected Task<bool> Delete(T entity, DbSet<T> dbSet)
        {
            return Task.Factory.StartNew(() =>
            {
                var result = dbSet.Remove(entity);

                return result.State == EntityState.Deleted;
            });
        }

        public virtual async Task<bool> Delete(T entity)
        {
            return await Delete(entity, DbSet);
        }

        public virtual async Task<bool> DeleteById(object id)
        {
            var dbSet = DbSet;
            var entity = await dbSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            return await Delete(entity, dbSet);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<T> Update(T entity)
        {
            return await Task.Factory.StartNew(() =>
            {
                DbSet.Attach(entity);

                _databaseContext
                    .Entry(entity)
                    .State = EntityState.Modified;

                return entity;
            });
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return DbSet.Filter(filter);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            return DbSet.IncludeProperties(includeProperties);
        }
    }
}
