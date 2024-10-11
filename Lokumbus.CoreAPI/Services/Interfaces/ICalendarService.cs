using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Definiert den Vertrag für Kalender-Service-Operationen.
    /// </summary>
    public interface ICalendarService
    {
        /// <summary>
        /// Ruft einen Kalender anhand seiner eindeutigen Kennung ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des Kalenders.</param>
        /// <returns>Den CalendarDto, wenn gefunden; andernfalls eine Ausnahme.</returns>
        Task<CalendarDto> GetByIdAsync(string id);
    
        /// <summary>
        /// Ruft alle Kalender ab.
        /// </summary>
        /// <returns>Eine Sammlung aller CalendarDtos.</returns>
        Task<IEnumerable<CalendarDto>> GetAllAsync();
    
        /// <summary>
        /// Erstellt einen neuen Kalender.
        /// </summary>
        /// <param name="createDto">Das DTO, das die Erstellungsdaten enthält.</param>
        /// <returns>Das erstellte CalendarDto.</returns>
        Task<CalendarDto> CreateAsync(CreateCalendarDto createDto);
    
        /// <summary>
        /// Aktualisiert einen bestehenden Kalender.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des zu aktualisierenden Kalenders.</param>
        /// <param name="updateDto">Das DTO, das die Aktualisierungsdaten enthält.</param>
        Task UpdateAsync(string id, UpdateCalendarDto updateDto);
    
        /// <summary>
        /// Löscht einen Kalender anhand seiner eindeutigen Kennung.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des zu löschenden Kalenders.</param>
        Task DeleteAsync(string id);
    }
}