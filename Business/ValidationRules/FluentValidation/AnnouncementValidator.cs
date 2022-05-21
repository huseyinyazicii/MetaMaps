using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AnnouncementValidator : AbstractValidator<Announcement>
    {
        public AnnouncementValidator()
        {
            RuleFor(x => x.Title).MaximumLength(100);
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).MaximumLength(500);
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
