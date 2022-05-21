using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Content).MaximumLength(150);
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
