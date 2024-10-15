using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces;

/// <summary>
/// Defines the contract for Reminder repository operations.
/// </summary>
public interface IReminderRepository
{
    /// <summary>
    /// Retrieves a Reminder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder.</param>
    /// <returns>The Reminder if found; otherwise, null.</returns>
    Task<Reminder?> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all Reminders.
    /// </summary>
    /// <returns>A collection of all Reminders.</returns>
    Task<IEnumerable<Reminder>> GetAllAsync();

    /// <summary>
    /// Creates a new Reminder.
    /// </summary>
    /// <param name="reminder">The Reminder to create.</param>
    Task CreateAsync(Reminder reminder);

    /// <summary>
    /// Updates an existing Reminder.
    /// </summary>
    /// <param name="reminder">The Reminder with updated information.</param>
    Task UpdateAsync(Reminder reminder);

    /// <summary>
    /// Deletes a Reminder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder to delete.</param>
    Task DeleteAsync(string id);
}