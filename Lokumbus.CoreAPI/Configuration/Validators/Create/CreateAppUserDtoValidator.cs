using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create;

/// <summary>
/// Validator for CreateAppUserDto using FluentValidation.
/// </summary>
public class CreateAppUserDtoValidator : AbstractValidator<CreateAppUserDto>
{
    /// <summary>
    /// Initializes a new instance of the CreateAppUserDtoValidator class.
    /// </summary>
    public CreateAppUserDtoValidator()
    {
        // Validate Username
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        // Validate Email
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        // Validate Password
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

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