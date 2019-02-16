using FluentValidation;
using Hadyach.Data.Contracts;
using Hadyach.Data.Entities.Categories;
using Hadyach.Dtos.Categories.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Hadyach.Validators.Categories.Base
{
    public class BaseCategoryDtoValidator : AbstractValidator<BaseCategoryDto>
    {
        private readonly IHadyachRepository<Category> categoryRepository;

        public BaseCategoryDtoValidator()
        {

        }

        public BaseCategoryDtoValidator(IHadyachRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;

            RuleFor(dto => dto.Title)
                .MinimumLength(1)
                .Must(BeUniq);

            RuleFor(dto => dto.ParentCategoryId)
                .GreaterThan(0)
                .Must(BeValid)
                .When(x => x.ParentCategoryId.HasValue);

            RuleFor(dto => dto.Description).MinimumLength(5);
        }

        private bool BeUniq(string title)
        {
            return !this.categoryRepository.GetMany(x => x.Title == title).Any();
        }

        private bool BeValid(int? id)
        {
            return this.categoryRepository.GetMany(x => x.Id == id.Value).Any();
        }
    }
}
