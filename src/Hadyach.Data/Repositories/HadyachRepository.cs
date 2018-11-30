using Hadyach.Common.Data.Repositories;
using Hadyach.Data.Contracts;

namespace Hadyach.Data.Repositories
{
    public class HadyachRepository<TEntity> : GenericRepository<TEntity>, IHadyachRepository<TEntity>
        where TEntity : class
    {
        public HadyachRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
