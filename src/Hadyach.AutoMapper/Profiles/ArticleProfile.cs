using AutoMapper;
using Hadyach.Data.Entities.Articles;
using Hadyach.Dtos.Articles;
using Hadyach.Dtos.Articles.Base;
using Hadyach.Models.Articles;
using Hadyach.Models.Articles.Base;

namespace Hadyach.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            // Add
            this.CreateMap<BaseArticleDto, BaseArticleModel>();

            this.CreateMap<AddArticleDto, AddArticleModel>()
                .IncludeBase<BaseArticleDto, BaseArticleModel>();

            this.CreateMap<AddArticleModel, Article>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Update
            this.CreateMap<UpdateArticleDto, UpdateArticleModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .IncludeBase<BaseArticleDto, BaseArticleModel>();

            // Get
            this.CreateMap<Article, BaseArticleDto>();

            this.CreateMap<Article, ArticleDto>()
                .IncludeBase<Article, BaseArticleDto>();
        }

    }
}
