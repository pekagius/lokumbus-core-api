using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces;

/// <summary>
/// Definiert den Vertrag für Repository-Operationen im Zusammenhang mit Reviews.
/// </summary>
public interface IReviewRepository
{
    /// <summary>
    /// Ruft ein Review anhand seiner eindeutigen Kennung ab.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des Reviews.</param>
    /// <returns>Das Review, sofern gefunden; andernfalls null.</returns>
    Task<Review?> GetByIdAsync(string id);
    
    /// <summary>
    /// Ruft alle Reviews ab.
    /// </summary>
    /// <returns>Eine Sammlung aller Reviews.</returns>
    Task<IEnumerable<Review>> GetAllAsync();
    
    /// <summary>
    /// Erstellt ein neues Review.
    /// </summary>
    /// <param name="review">Das zu erstellende Review.</param>
    Task CreateAsync(Review review);
    
    /// <summary>
    /// Aktualisiert ein bestehendes Review.
    /// </summary>
    /// <param name="review">Das Review mit aktualisierten Informationen.</param>
    Task UpdateAsync(Review review);
    
    /// <summary>
    /// Löscht ein Review anhand seiner eindeutigen Kennung.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des Reviews, das gelöscht werden soll.</param>
    Task DeleteAsync(string id);
}