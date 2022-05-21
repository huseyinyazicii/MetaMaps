using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RoadMapValidator : AbstractValidator<RoadMap>
    {
        public RoadMapValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
