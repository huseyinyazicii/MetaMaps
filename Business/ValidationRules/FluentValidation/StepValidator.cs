using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class StepValidator : AbstractValidator<Step>
    {
        public StepValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
