using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hadyach.Common.Exceptions;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities;
using Hadyach.Models.Articles;
using Hadyach.Services.Contracts.Services.Articles;
using Hadyach.Services.Contracts.Services.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hadyach.Services.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IHadyachRepository<Article> articleRepository;
        private readonly IHadyachRepository<ArticleTag> articleTagRepository;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly ILogger<ArticleService> logger;

        public ArticleService(
            IHadyachRepository<Article> articleRepository,
            IHadyachRepository<ArticleTag> articleTagRepository,
            ICategoryService categoryService,
            IMapper mapper,
            ILogger<ArticleService> logger)
        {
            this.articleRepository = articleRepository;
            this.articleTagRepository = articleTagRepository;
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

            //foreach(var tag in model.Tags)
            //{
            //    await this.articleTagRepository.AddAsync(new ArticleTag { ArticleId = article.Id, Tag = tag });
            //}

            return await GetAsync<TResult>(article.Id);
        }

        public async Task<TResult> GetAsync<TResult>(int id)
        {
            var result = await this.articleRepository
                .GetMany(p => p.Id == id,
                    include => include.ArticleTags)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new NotFoundException(id);
            }

            return this.mapper.Map<TResult>(result);
        }

        public async Task<ICollection<TResult>> GetManyAsync<TResult>(int skip = 0, int top = 10,
            int? categoryId = null, string tag = null)
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
                      || (categoryId.HasValue && article.CategoryId.HasValue && categoryIds.Contains(article.CategoryId.Value)))
                    && (string.IsNullOrWhiteSpace(tag)
                      || (!string.IsNullOrWhiteSpace(tag) && article.ArticleTags.Any(x => x.Tag.Value == tag))));
        }

        public async Task<TResult> UpdateAsync<TResult>(UpdateArticleModel model)
        {
            var updatedEntity = this.mapper.Map<Article>(model);

            updatedEntity.ModifiedDateTime = DateTime.Now;

            this.articleRepository.Update(updatedEntity);
            await this.articleRepository.SaveAsync();

            var existedTags = await this.articleTagRepository.GetMany(x => x.ArticleId == model.Id).ToListAsync();
            this.articleTagRepository.DeleteRange(existedTags);

            //foreach (var tag in model.Tags)
            //{
            //    await this.articleTagRepository.AddAsync(new ArticleTag { ArticleId = model.Id, Tag = tag });
            //}

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
                .GetMany(predicate,
                    include => include.ArticleTags.Select(t => t.Tag))
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
