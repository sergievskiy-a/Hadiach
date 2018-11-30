using System;
using System.Linq;
using System.Linq.Expressions;
using Hadyach.Common.Data.Contracts.Repositories;

namespace Hadyach.Common.Data.Extensions
{
    public static class GenericRepositoryExtensions
    {
        public static bool DoesExists<T>(this IGenericRepository<T> repository, Expression<Func<T, bool>> predicate)
            where T : class
        {
            return repository.GetMany(predicate).Any();
        }

        public static bool DoesntExists<T>(this IGenericRepository<T> repository, Expression<Func<T, bool>> predicate)
            where T : class
        {
            return !repository.DoesExists(predicate);
        }
    }
}
