using System.Collections.Generic;
using System.Threading.Tasks;
using Hadyach.Models.Categories;

namespace Hadyach.Services.Contracts.Services.Categories
{
    public interface ICategoryService
    {
        Task<TResult> AddAsync<TResult>(AddCategoryModel model);

        Task<TResult> GetAsync<TResult>(int id);

        Task<ICollection<TResult>> GetManyAsync<TResult>(int skip = 0, int top = 10);

        Task<TResult> UpdateAsync<TResult>(UpdateCategoryModel model);

        Task<ICollection<int>> GetCategoryWithChildIds(int id);

        Task DeleteAsync(int id);
    }
}
