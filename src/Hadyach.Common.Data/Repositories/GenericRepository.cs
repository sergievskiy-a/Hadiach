using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hadyach.Common.Data.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hadyach.Common.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext Context;

        protected GenericRepository(DbContext context)
        {
            this.Context = context;
        }

        private DbSet<TEntity> Entities => this.Context.Set<TEntity>();

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] properties)
        {
            return predicate != null ?
                await this.GetQueryWithIncludes(properties).Where(predicate).SingleOrDefaultAsync() :
                await this.GetQueryWithIncludes(properties).SingleOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] properties)
        {
            return predicate != null ?
                this.GetQueryWithIncludes(properties).Where(predicate) :
                this.GetQueryWithIncludes(properties);
        }

        public async Task AddAsync(TEntity entity)
        {
            await this.Entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await this.Entities.AddRangeAsync(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            this.Entities.RemoveRange(entities);
        }

        public void Delete(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        protected IQueryable<TEntity> GetQueryWithIncludes(params Expression<Func<TEntity, object>>[] properties)
        {
            var query = this.Entities as IQueryable<TEntity>;
            if (properties != null)
            {
                query = properties.Aggregate(query, (current, property) => current.Include(property));
            }

            return query;
        }
    }
}
