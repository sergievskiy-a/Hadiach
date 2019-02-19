using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities;
using Hadyach.Dtos.Categories;

namespace Hadyach.Services.Resolvers
{
    public class ParentCategoriesResolver : IMemberValueResolver<object, object, int?, CategoryDto>
    {
        private readonly IHadyachRepository<Category> categoryRepository;
        private readonly IMapper mapper;

        public ParentCategoriesResolver(IHadyachRepository<Category> categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public CategoryDto Resolve(object source, object destination, int? categoryId, CategoryDto destMember, ResolutionContext context)
        {
            if (!categoryId.HasValue)
            {
                return null;
            }

            return GetCategory(categoryId.Value);
        }

        private CategoryDto GetCategory(int id)
        {
            var category = this.categoryRepository
                .GetMany(x => x.Id == id)
                .ProjectTo<CategoryDto>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            if (category.ParentCategoryId.HasValue)
            {
                category.ParentCategory = GetCategory(category.ParentCategoryId.Value);
            }

            return category;
        }
    }
}
