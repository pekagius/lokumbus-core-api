using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Auth;

namespace Lokumbus.CoreAPI.Configuration.Validators.Auth
{
    /// <summary>
    /// Validator für TokenDto.
    /// </summary>
    public class TokenDtoValidator : AbstractValidator<TokenDto>
    {
        public TokenDtoValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("AccessToken ist erforderlich.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("RefreshToken ist erforderlich.");
        }
    }
}