using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hadyach.Dtos.Articles;
using Hadyach.Models.Articles;

namespace Hadyach.Services.Contracts.Services.Articles
{
    public interface IArticleService
    {
        Task<TResult> AddAsync<TResult>(AddArticleModel model);

        TResult Get<TResult>(int id);

        ICollection<TResult> GetMany<TResult>();

        Task UpdateAsync(AddArticleModel model);

        Task DeleteAsync(int id);
    }
}
