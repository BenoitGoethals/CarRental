using FluentValidation;
using Shared.model;

namespace Shared.validator
{
    public class TimeSlotValidator: AbstractValidator<TimeSlot>
    {
        public TimeSlotValidator()
        {
            RuleFor(x => x.StartSlot).NotEmpty();
            RuleFor(x => x.EndSlot).NotEmpty();
            RuleFor(x => x.EndSlot).GreaterThan(y => y.StartSlot);

        }
    }
}