using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create;

/// <summary>
/// Validator für <see cref="CreateReviewDto"/> unter Verwendung von FluentValidation.
/// </summary>
public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="CreateReviewDtoValidator"/>-Klasse.
    /// </summary>
    public CreateReviewDtoValidator()
    {
        // Validierung der PersonaId
        RuleFor(x => x.PersonaId)
            .NotEmpty().WithMessage("PersonaId ist erforderlich.");
        
        // Validierung der ReviewedEntityId
        RuleFor(x => x.ReviewedEntityId)
            .NotEmpty().WithMessage("ReviewedEntityId ist erforderlich.");
        
        // Validierung des ReviewedEntityType
        RuleFor(x => x.ReviewedEntityType)
            .NotEmpty().WithMessage("ReviewedEntityType ist erforderlich.");
        
        // Validierung der Rating
        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage("Rating ist erforderlich.")
            .Must(r => int.TryParse(r, out int value) && value >= 1 && value <= 5)
            .WithMessage("Rating muss ein numerischer Wert zwischen 1 und 5 sein.");
        
        // Validierung des Comments
        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage("Comment darf maximal 1000 Zeichen lang sein.")
            .When(x => !string.IsNullOrEmpty(x.Comment));
    }
}