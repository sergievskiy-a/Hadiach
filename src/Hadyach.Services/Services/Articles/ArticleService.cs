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
using Microsoft.EntityFrameworkCore;
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
            IMapper mapper,
            ILogger<ArticleService> logger)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<TResult> AddAsync<TResult>(AddArticleModel model)
        {
            var article = this.mapper.Map<Article>(model);
            
            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveAsync();

            return await GetAsync<TResult>(article.Id);
        }

        public async Task<TResult> GetAsync<TResult>(int id)
        {
            return await this.articleRepository
                .GetMany(p => p.Id == id)
                .ProjectTo<TResult>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<TResult>> GetManyAsync<TResult>(int skip = 0, int top = 10)
        {
            return await this.GetManyInternalAsync<TResult>(skip, top);
        }

        public async Task UpdateAsync(AddArticleModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await this.articleRepository.GetSingleAsync(x => x.Id == id);
            
            this.articleRepository.Delete(article);
            await this.articleRepository.SaveAsync();
        }

        private async Task<ICollection<TResult>> GetManyInternalAsync<TResult>(
            int skip, int top, Expression<Func<Article, bool>> predicate = null)
        {
            return await this.articleRepository
                .GetMany(predicate)
                .Skip(skip)
                .Take(top)
                .ProjectTo<TResult>(this.mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
