using AutoMapper;
using Hadyach.Data.Entities.Articles;
using Hadyach.Dtos.Articles;
using Hadyach.Models.Articles;

namespace Hadyach.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            this.CreateMap<Article, ArticleDto>();

            this.CreateMap<AddArticleDto, AddArticleModel>();

            this.CreateMap<AddArticleModel, Article>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
