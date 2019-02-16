using FluentValidation;
using Hadyach.Dtos.Categories;
using Hadyach.Dtos.Categories.Base;

namespace Hadyach.Validators.Categories
{
    public class AddCategoryDtoValidator : AbstractValidator<AddCategoryDto>
    {
        public AddCategoryDtoValidator()
        {
        }

        public AddCategoryDtoValidator(IValidator<BaseCategoryDto> baseValidator)
        {
            this.Include(baseValidator);
        }
    }
}
