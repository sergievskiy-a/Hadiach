using AutoMapper;
using Hadyach.Data.Entities;
using Hadyach.Dtos.Articles;
using Hadyach.Dtos.Articles.Base;
using Hadyach.Models.Articles;
using Hadyach.Models.Articles.Base;
using Hadyach.Services.Resolvers;
using System.Collections.Generic;
using System.Linq;

namespace Hadyach.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            // Add
            this.CreateMap<BaseArticleDto, BaseArticleModel>();

            this.CreateMap<AddArticleDto, AddArticleModel>()
                .IncludeBase<BaseArticleDto, BaseArticleModel>()
                .ForMember(dest => dest.PublishedDateTime, opt => opt.MapFrom(src => src.PublishedDateTime));

            this.CreateMap<AddArticleModel, Article>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // Update
            this.CreateMap<UpdateArticleDto, UpdateArticleModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PublishedDateTime, opt => opt.MapFrom(src => src.PublishedDateTime))
                .IncludeBase<BaseArticleDto, BaseArticleModel>();

            this.CreateMap<UpdateArticleModel, Article>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // Get
            this.CreateMap<Article, BaseArticleDto>();

            this.CreateMap<Article, ArticleDto>()
                .IncludeBase<Article, BaseArticleDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom<TagsResolver, List<ArticleTag>>(src => src.ArticleTags));
        }

    }
}
