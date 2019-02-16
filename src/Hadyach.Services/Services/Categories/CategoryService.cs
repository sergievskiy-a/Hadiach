using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hadyach.Common.Exceptions;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities.Categories;
using Hadyach.Models.Categories;
using Hadyach.Services.Contracts.Services.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hadyach.Services.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IHadyachRepository<Category> categoryRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CategoryService> logger;

        public CategoryService(
            IHadyachRepository<Category> categoryRepository,
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<TResult> AddAsync<TResult>(AddCategoryModel model)
        {
            var category = this.mapper.Map<Category>(model);
            
            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveAsync();

            return await GetAsync<TResult>(category.Id);
        }

        public async Task<TResult> GetAsync<TResult>(int id)
        {
            var result = await this.categoryRepository
                .GetMany(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new NotFoundException(id);
            }

            return this.mapper.Map<TResult>(result);
        }

        public async Task<ICollection<TResult>> GetManyAsync<TResult>(int skip = 0, int top = 10)
        {
            return await this.GetManyInternalAsync<TResult>(skip, top);
        }

        public async Task<TResult> UpdateAsync<TResult>(UpdateCategoryModel model)
        {
            var updatedEntity = this.mapper.Map<Category>(model);
            this.categoryRepository.Update(updatedEntity);
            await this.categoryRepository.SaveAsync();

            return await GetAsync<TResult>(updatedEntity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await this.categoryRepository.GetSingleAsync(x => x.Id == id);
            
            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveAsync();
        }

        public async Task<ICollection<int>> GetCategoryWithChildIds(int id)
        {
            var result = new List<int> { id };

            var childs = await this.categoryRepository
                .GetMany(x => x.ParentCategoryId == id)
                .Select(x => x.Id)
                .ToListAsync();

            foreach (var child in childs)
            {
                result.AddRange(await GetCategoryWithChildIds(child));
            }

            return result;
        }

        private async Task<ICollection<TResult>> GetManyInternalAsync<TResult>(
            int skip, int top, Expression<Func<Category, bool>> predicate = null)
        {
            return await this.categoryRepository
                .GetMany(predicate)
                .Skip(skip)
                .Take(top)
                .ProjectTo<TResult>(this.mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
