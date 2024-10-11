using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator for UpdateCalendarEventAttendeeDto using FluentValidation.
    /// </summary>
    public class UpdateCalendarEventAttendeeDtoValidator : AbstractValidator<UpdateCalendarEventAttendeeDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateCalendarEventAttendeeDtoValidator class.
        /// </summary>
        public UpdateCalendarEventAttendeeDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            // Validate Email
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            // Validate Status
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid attendee status.")
                .When(x => x.Status.HasValue);
        }
    }
}