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
        }
    }
}
