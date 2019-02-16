using FluentValidation;
using Hadyach.Dtos.Categories;
using Hadyach.Dtos.Categories.Base;

namespace Hadyach.Validators.Categories
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
        }

        public UpdateCategoryDtoValidator(IValidator<BaseCategoryDto> baseValidator)
        {
            this.Include(baseValidator);
        }
    }
}
