using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs;

/// <summary>
/// Data Transfer Object, das ein Review repräsentiert.
/// </summary>
public class ReviewDto
{
    /// <summary>
    /// Die eindeutige Kennung des Reviews.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Die Kennung der Persona, die das Review abgibt.
    /// </summary>
    public string PersonaId { get; set; }
    
    /// <summary>
    /// Der Name der Persona, die das Review abgibt.
    /// </summary>
    public string? PersonaName { get; set; }
    
    /// <summary>
    /// Die eindeutige Kennung der zu bewertenden Entität.
    /// </summary>
    public string ReviewedEntityId { get; set; }
    
    /// <summary>
    /// Der Typ der zu bewertenden Entität.
    /// </summary>
    public string? ReviewedEntityType { get; set; }
    
    /// <summary>
    /// Die Bewertung.
    /// </summary>
    public string Rating { get; set; }
    
    /// <summary>
    /// Der Kommentar zum Review.
    /// </summary>
    public string? Comment { get; set; }
    
    /// <summary>
    /// Das Datum und die Uhrzeit, wann das Review erstellt wurde.
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// Das Datum und die Uhrzeit, wann das Review zuletzt aktualisiert wurde.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Gibt an, ob das Review aktiv ist.
    /// </summary>
    public bool? IsActive { get; set; }
}