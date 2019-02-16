using FluentValidation;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities.Categories;
using Hadyach.Dtos.Articles.Base;
using System.Linq;

namespace Hadyach.Validators.Articles.Base
{
    public class BaseArticleDtoValidator : AbstractValidator<BaseArticleDto>
    {
        private readonly IHadyachRepository<Category> categoryRepository;

        public BaseArticleDtoValidator()
        {
        }

        public BaseArticleDtoValidator(IHadyachRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;

            RuleFor(dto => dto.Title).MinimumLength(1);

            RuleFor(dto => dto.CategoryId)
                .GreaterThan(0)
                .Must(BeExistedCategory)
                .When(x => x.CategoryId.HasValue);

            RuleFor(dto => dto.Description).MinimumLength(5);
        }

        private bool BeExistedCategory(int? categoryId)
        {
            return this.categoryRepository.GetMany(x => x.Id == categoryId.Value).Any();
        }
    }
}
