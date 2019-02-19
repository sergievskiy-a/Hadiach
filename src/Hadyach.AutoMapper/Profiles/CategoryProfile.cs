using AutoMapper;
using Hadyach.Data.Entities;
using Hadyach.Dtos.Categories;
using Hadyach.Dtos.Categories.Base;
using Hadyach.Models.Categories;
using Hadyach.Models.Categories.Base;
using Hadyach.Services.Resolvers;

namespace Hadyach.AutoMapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Add
            this.CreateMap<BaseCategoryDto, BaseCategoryModel>();

            this.CreateMap<AddCategoryDto, AddCategoryModel>()
                .IncludeBase<BaseCategoryDto, BaseCategoryModel>();

            this.CreateMap<AddCategoryModel, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCategory, opt => opt.Ignore());

            // Update
            this.CreateMap<UpdateCategoryDto, UpdateCategoryModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .IncludeBase<BaseCategoryDto, BaseCategoryModel>();

            this.CreateMap<UpdateCategoryModel, Category>()
                .ForMember(dest => dest.ParentCategory, opt => opt.Ignore());

            // Get
            this.CreateMap<Category, BaseCategoryDto>();

            this.CreateMap<Category, CategoryDto>()
                .IncludeBase<Category, BaseCategoryDto>()
                .ForMember(dest => dest.ParentCategory, opt => opt.MapFrom<ParentCategoriesResolver, int?>(src => src.ParentCategoryId));
        }

    }
}
