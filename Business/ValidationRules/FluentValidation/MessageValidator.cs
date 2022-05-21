using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Topic).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.UserMail).MaximumLength(100);
            RuleFor(x => x.UserName).MaximumLength(100);
            RuleFor(x => x.Topic).MaximumLength(50);
            RuleFor(x => x.Content).MaximumLength(500);
        }
    }
}
