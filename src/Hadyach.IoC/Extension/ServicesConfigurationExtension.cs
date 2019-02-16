using FluentValidation;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities.Articles;
using Hadyach.Data.Entities.Categories;
using Hadyach.Data.Repositories;
using Hadyach.Dtos.Articles;
using Hadyach.Dtos.Articles.Base;
using Hadyach.Dtos.Categories;
using Hadyach.Dtos.Categories.Base;
using Hadyach.Services.Contracts.Services.Articles;
using Hadyach.Services.Contracts.Services.Categories;
using Hadyach.Services.Resolvers;
using Hadyach.Services.Services.Articles;
using Hadyach.Services.Services.Categories;
using Hadyach.Validators.Articles;
using Hadyach.Validators.Articles.Base;
using Hadyach.Validators.Categories;
using Hadyach.Validators.Categories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Hadyach.IoC.Extensions
{
    public static class ServicesConfigurationExtension
    {
        public static IServiceCollection RegisterHadiachServices(this IServiceCollection services)
        {
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICategoryService, CategoryService>();

            return services;
        }

        public static IServiceCollection RegisterHadiachRepositories(this IServiceCollection services)
        {
            services.AddTransient<IHadyachRepository<Article>, HadyachRepository<Article>>();
            services.AddTransient<IHadyachRepository<Category>, HadyachRepository<Category>>();

            return services;
        }

        public static IServiceCollection RegisterHadiachValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<BaseArticleDto>, BaseArticleDtoValidator>();
            services.AddTransient<IValidator<AddArticleDto>, AddArticleDtoValidator>();
            services.AddTransient<IValidator<UpdateArticleDto>, UpdateArticleDtoValidator>();
            services.AddTransient<IValidator<BaseCategoryDto>, BaseCategoryDtoValidator>();
            services.AddTransient<IValidator<AddCategoryDto>, AddCategoryDtoValidator>();
            services.AddTransient<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();

            services.AddTransient(typeof(ParentCategoriesResolver));

            return services;
        }
    }
}
