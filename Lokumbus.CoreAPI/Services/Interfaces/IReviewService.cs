using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces;

/// <summary>
/// Definiert den Vertrag für Service-Operationen im Zusammenhang mit Reviews.
/// </summary>
public interface IReviewService
{
    /// <summary>
    /// Ruft ein Review anhand seiner eindeutigen Kennung ab.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des Reviews.</param>
    /// <returns>Das ReviewDto, sofern gefunden; andernfalls null.</returns>
    Task<ReviewDto?> GetByIdAsync(string id);
    
    /// <summary>
    /// Ruft alle Reviews ab.
    /// </summary>
    /// <returns>Eine Sammlung aller ReviewDtos.</returns>
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    
    /// <summary>
    /// Erstellt ein neues Review.
    /// </summary>
    /// <param name="createDto">Das DTO, das die Daten für das neue Review enthält.</param>
    /// <returns>Das erstellte ReviewDto.</returns>
    Task<ReviewDto> CreateAsync(CreateReviewDto createDto);
    
    /// <summary>
    /// Aktualisiert ein bestehendes Review.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des zu aktualisierenden Reviews.</param>
    /// <param name="updateDto">Das DTO, das die aktualisierten Daten enthält.</param>
    Task UpdateAsync(string id, UpdateReviewDto updateDto);
    
    /// <summary>
    /// Löscht ein Review anhand seiner eindeutigen Kennung.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des zu löschenden Reviews.</param>
    Task DeleteAsync(string id);
}