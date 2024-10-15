using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators;

/// <summary>
/// Validator for <see cref="CreateReminderDto"/> using FluentValidation.
/// </summary>
public class CreateReminderDtoValidator : AbstractValidator<CreateReminderDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReminderDtoValidator"/> class.
    /// </summary>
    public CreateReminderDtoValidator()
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