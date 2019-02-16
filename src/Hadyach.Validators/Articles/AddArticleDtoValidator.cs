using FluentValidation;
using Hadyach.Dtos.Articles;
using Hadyach.Dtos.Articles.Base;

namespace Hadyach.Validators.Articles
{
    public class AddArticleDtoValidator : AbstractValidator<AddArticleDto>
    {
        public AddArticleDtoValidator()
        {
        }

        public AddArticleDtoValidator(IValidator<BaseArticleDto> baseValidator)
        {
            this.Include(baseValidator);
        }
    }
}
