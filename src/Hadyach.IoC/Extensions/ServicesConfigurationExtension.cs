using Hadyach.Data.Contracts;
using Hadyach.Data.Entities.Articles;
using Hadyach.Data.Repositories;
using Hadyach.Services.Contracts.Services.Articles;
using Hadyach.Services.Services.Articles;
using Microsoft.Extensions.DependencyInjection;

namespace Hadyach.IoC.Extensions
{
    public static class ServicesConfigurationExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IHadyachRepository<Article>, HadyachRepository<Article>>();
        }
    }
}
