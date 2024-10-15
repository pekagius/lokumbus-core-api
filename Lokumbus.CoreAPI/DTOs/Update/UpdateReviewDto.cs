using JetBrains.Annotations;

namespace Lokumbus.CoreAPI.DTOs.Update;

/// <summary>
/// Data Transfer Object für die Aktualisierung eines bestehenden Reviews.
/// </summary>
[UsedImplicitly]
public class UpdateReviewDto
{
    /// <summary>
    /// Die Bewertung als numerischer Wert (z.B. 1-5).
    /// </summary>
    public string? Rating { get; set; }
    
    /// <summary>
    /// Der Kommentar zum Review.
    /// </summary>
    public string? Comment { get; set; }
}