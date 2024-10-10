using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Auth;

namespace Lokumbus.CoreAPI.Configuration.Validators.Auth
{
    /// <summary>
    /// Validator für LoginDto.
    /// </summary>
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email ist erforderlich.")
                .EmailAddress().WithMessage("Ungültige Email-Adresse.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Passwort ist erforderlich.");
        }
    }
}