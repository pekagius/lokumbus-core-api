using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update;

/// <summary>
/// Validator for <see cref="UpdateReminderDto"/> using FluentValidation.
/// </summary>
public class UpdateReminderDtoValidator : AbstractValidator<UpdateReminderDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateReminderDtoValidator"/> class.
    /// </summary>
    public UpdateReminderDtoValidator()
    {
        // Validate Minutes
        RuleFor(x => x.Minutes)
            .GreaterThanOrEqualTo(0).WithMessage("Minutes must be a non-negative number.")
            .When(x => x.Minutes.HasValue);

        // Validate Hours
        RuleFor(x => x.Hours)
            .GreaterThanOrEqualTo(0).WithMessage("Hours must be a non-negative number.")
            .When(x => x.Hours.HasValue);

        // Validate Days
        RuleFor(x => x.Days)
            .GreaterThanOrEqualTo(0).WithMessage("Days must be a non-negative number.")
            .When(x => x.Days.HasValue);
    }
}