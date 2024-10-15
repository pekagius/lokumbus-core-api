using JetBrains.Annotations;

namespace Lokumbus.CoreAPI.DTOs.Create;

/// <summary>
/// Data Transfer Object für die Erstellung eines neuen Reviews.
/// </summary>
[UsedImplicitly]
public class CreateReviewDto
{
    /// <summary>
    /// Die eindeutige Kennung der Persona, die das Review abgibt.
    /// </summary>
    public string PersonaId { get; set; }
    
    /// <summary>
    /// Die eindeutige Kennung der zu bewertenden Entität (z.B. Event, Location).
    /// </summary>
    public string ReviewedEntityId { get; set; }
    
    /// <summary>
    /// Der Typ der zu bewertenden Entität (z.B. "Event", "Location").
    /// </summary>
    public string? ReviewedEntityType { get; set; }
    
    /// <summary>
    /// Die Bewertung als numerischer Wert (z.B. 1-5).
    /// </summary>
    public string Rating { get; set; }
    
    /// <summary>
    /// Der Kommentar zum Review.
    /// </summary>
    public string? Comment { get; set; }
}