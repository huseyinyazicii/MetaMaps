using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CreaterValidator : AbstractValidator<Creater>
    {
        public CreaterValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Details).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Surname).MaximumLength(50);
            RuleFor(x => x.Email).MaximumLength(100);
            RuleFor(x => x.Details).MaximumLength(200);
        }
    }
}
