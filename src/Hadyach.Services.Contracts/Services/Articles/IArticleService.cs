﻿using System;
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

        Task<TResult> GetAsync<TResult>(int id);

        Task<ICollection<TResult>> GetManyAsync<TResult>(int skip = 0, int top = 10, int? categoryId = null, string tag = null);

        Task<TResult> UpdateAsync<TResult>(UpdateArticleModel model);

        Task DeleteAsync(int id);
    }
}
