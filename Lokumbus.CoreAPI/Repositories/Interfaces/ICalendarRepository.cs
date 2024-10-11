using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Definiert den Vertrag für Kalender-Repository-Operationen.
    /// </summary>
    public interface ICalendarRepository
    {
        /// <summary>
        /// Ruft einen Kalender anhand seiner eindeutigen Kennung ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des Kalenders.</param>
        /// <returns>Den Kalender, wenn gefunden; andernfalls null.</returns>
        Task<Calendar?> GetByIdAsync(string id);
    
        /// <summary>
        /// Ruft alle Kalender ab.
        /// </summary>
        /// <returns>Eine Sammlung aller Kalender.</returns>
        Task<IEnumerable<Calendar>> GetAllAsync();
    
        /// <summary>
        /// Erstellt einen neuen Kalender.
        /// </summary>
        /// <param name="calendar">Der zu erstellende Kalender.</param>
        Task CreateAsync(Calendar calendar);
    
        /// <summary>
        /// Aktualisiert einen bestehenden Kalender.
        /// </summary>
        /// <param name="calendar">Der Kalender mit aktualisierten Informationen.</param>
        Task UpdateAsync(Calendar calendar);
    
        /// <summary>
        /// Löscht einen Kalender anhand seiner eindeutigen Kennung.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des zu löschenden Kalenders.</param>
        Task DeleteAsync(string id);
    }
}