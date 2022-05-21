using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SourceValidator : AbstractValidator<Source>
    {
        public SourceValidator()
        {
            RuleFor(x => x.Link).NotEmpty();
            RuleFor(x => x.Content).MaximumLength(500);
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
