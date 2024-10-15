using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators;

/// <summary>
/// Validator für <see cref="UpdateReviewDto"/> unter Verwendung von FluentValidation.
/// </summary>
public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="UpdateReviewDtoValidator"/>-Klasse.
    /// </summary>
    public UpdateReviewDtoValidator()
    {
        // Validierung der Rating
        RuleFor(x => x.Rating)
            .Must(r => int.TryParse(r, out int value) && value >= 1 && value <= 5)
            .WithMessage("Rating muss ein numerischer Wert zwischen 1 und 5 sein.")
            .When(x => !string.IsNullOrEmpty(x.Rating));

        // Validierung des Comments
        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage("Comment darf maximal 1000 Zeichen lang sein.")
            .When(x => !string.IsNullOrEmpty(x.Comment));
    }
}