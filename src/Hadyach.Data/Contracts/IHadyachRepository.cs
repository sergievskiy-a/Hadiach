using Hadyach.Common.Data.Contracts.Repositories;

namespace Hadyach.Data.Contracts
{
    public interface IHadyachRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
    }
}
