using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities.Articles;
using Hadyach.Dtos.Articles;
using Hadyach.Models.Articles;
using Hadyach.Services.Contracts.Services.Articles;
using Microsoft.Extensions.Logging;

namespace Hadyach.Services.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IHadyachRepository<Article> articleRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ArticleService> logger;

        public ArticleService(
            IHadyachRepository<Article> articleRepository,
            ILogger<ArticleService> logger)
        {
            this.articleRepository = articleRepository;
            this.logger = logger;
        }

        public Task<TResult> AddAsync<TResult>(AddArticleModel model)
        {
            throw new System.NotImplementedException();
        }

        public TResult Get<TResult>(int id)
        {
            return this.articleRepository
                .GetMany(p => p.Id == id)
                .ProjectTo<TResult>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public ICollection<TResult> GetMany<TResult>()
        {
            return this.GetManyInternal<TResult>();
        }

        public Task UpdateAsync(AddArticleModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        private ICollection<TResult> GetManyInternal<TResult>(
            int skip = 0, int top = 10, Expression<Func<Article, bool>> predicate = null)
        {
            return this.articleRepository
                .GetMany(predicate)
                .Skip(skip)
                .Take(top)
                .ProjectTo<TResult>(this.mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
