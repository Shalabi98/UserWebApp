using FluentValidation;
using UserWebApp.Models;

namespace UserWebApp.Validators
{
    public class StreamingValidator : AbstractValidator<Streaming>
    {
        public StreamingValidator()
        {
            RuleFor(s => s.Channel)
                .NotEmpty()
                .InclusiveBetween(1, 10);

            RuleFor(s => s.Description)
                .MaximumLength(255);

            RuleFor(s => s.Detail.Duration)
                .NotEmpty();

            RuleFor(s => s.Detail.Size)
                .NotEmpty();

            RuleFor(s => s.Detail.Type)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
