using FluentValidation;
using Hadyach.Dtos.Articles;

namespace Hadyach.Validators.Articles
{
    public class AddArticleDtoValidator : AbstractValidator<AddArticleDto>
    {
        public AddArticleDtoValidator()
        {
            RuleFor(dto => dto.Title).MinimumLength(1);
            RuleFor(dto => dto.Description).MinimumLength(5);
        }
    }
}
