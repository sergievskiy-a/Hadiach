using FluentValidation;
using Hadyach.Dtos.Articles;
using Hadyach.Dtos.Articles.Base;

namespace Hadyach.Validators.Articles
{
    public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleDtoValidator()
        {
        }

        public UpdateArticleDtoValidator(IValidator<BaseArticleDto> baseValidator)
        {
            this.Include(baseValidator);
        }
    }
}
