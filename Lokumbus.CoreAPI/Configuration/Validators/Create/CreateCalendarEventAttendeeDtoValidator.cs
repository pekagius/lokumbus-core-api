using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator for CreateCalendarEventAttendeeDto using FluentValidation.
    /// </summary>
    public class CreateCalendarEventAttendeeDtoValidator : AbstractValidator<CreateCalendarEventAttendeeDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateCalendarEventAttendeeDtoValidator class.
        /// </summary>
        public CreateCalendarEventAttendeeDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            // Validate Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            // Validate Status
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid attendee status.");
        }
    }
}