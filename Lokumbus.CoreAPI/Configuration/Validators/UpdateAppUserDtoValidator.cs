using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators;

/// <summary>
/// Validator for UpdateAppUserDto using FluentValidation.
/// </summary>
public class UpdateAppUserDtoValidator : AbstractValidator<UpdateAppUserDto>
{
    /// <summary>
    /// Initializes a new instance of the UpdateAppUserDtoValidator class.
    /// </summary>
    public UpdateAppUserDtoValidator()
    {
        // Validate Username
        RuleFor(x => x.Username)
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        // Validate Email
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email address.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        // Validate Password
        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .When(x => !string.IsNullOrEmpty(x.Password));

        // Validate FirstName
        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        // Validate LastName
        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        // Validate Phone
        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");

        // Additional validation rules can be added here as needed.
    }
}