using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hadyach.Common.Exceptions;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities.Articles;
using Hadyach.Dtos.Articles;
using Hadyach.Models.Articles;
using Hadyach.Models.Articles.Base;
using Hadyach.Services.Contracts.Services.Articles;
using Hadyach.Services.Contracts.Services.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hadyach.Services.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IHadyachRepository<Article> articleRepository;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly ILogger<ArticleService> logger;

        public ArticleService(
            IHadyachRepository<Article> articleRepository,
            ICategoryService categoryService,
            IMapper mapper,
            ILogger<ArticleService> logger)
        {
            this.articleRepository = articleRepository;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<TResult> AddAsync<TResult>(AddArticleModel model)
        {
            var article = this.mapper.Map<Article>(model);

            article.CreatedDateTime = DateTime.Now;

            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveAsync();

            return await GetAsync<TResult>(article.Id);
        }

        public async Task<TResult> GetAsync<TResult>(int id)
        {
            var result = await this.articleRepository
                .GetMany(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new NotFoundException(id);
            }

            return this.mapper.Map<TResult>(result);
        }

        public async Task<ICollection<TResult>> GetManyAsync<TResult>(int skip = 0, int top = 10, int? categoryId = null)
        {
            List<int> categoryIds = new List<int>();
            if(categoryId.HasValue)
            {
                categoryIds.AddRange(await this.categoryService.GetCategoryWithChildIds(categoryId.Value));
            }

            return await this.GetManyInternalAsync<TResult>(
                skip,
                top,
                article =>
                    (!categoryId.HasValue
                      || (categoryId.HasValue && article.CategoryId.HasValue && categoryIds.Contains(article.CategoryId.Value))));
        }

        public async Task<TResult> UpdateAsync<TResult>(UpdateArticleModel model)
        {
            var updatedEntity = this.mapper.Map<Article>(model);

            updatedEntity.ModifiedDateTime = DateTime.Now;

            this.articleRepository.Update(updatedEntity);
            await this.articleRepository.SaveAsync();

            return await GetAsync<TResult>(updatedEntity.Id);
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
            var query = this.articleRepository
                .GetMany(predicate)
                .Skip(skip)
                .Take(top)
                .Where(x => x.PublishedDateTime <= DateTime.Now)
                .OrderByDescending(x => x.Pinned).ThenByDescending(x => x.PublishedDateTime);

            return await query
                .ProjectTo<TResult>(this.mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
