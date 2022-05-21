using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BranchValidator : AbstractValidator<Branch>
    {
        public BranchValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
