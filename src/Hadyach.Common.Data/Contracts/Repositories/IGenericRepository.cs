using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hadyach.Common.Data.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] properties);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] properties);
        void Save();
        Task SaveAsync();
        void Update(TEntity entity);
    }
}
