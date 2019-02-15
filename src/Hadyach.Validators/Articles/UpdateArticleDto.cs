using FluentValidation;
using Hadyach.Dtos.Articles;

namespace Hadyach.Validators.Articles
{
    public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleDtoValidator()
        {
            RuleFor(dto => dto.Title).MinimumLength(1);
            RuleFor(dto => dto.Description).MinimumLength(5);
        }
    }
}
