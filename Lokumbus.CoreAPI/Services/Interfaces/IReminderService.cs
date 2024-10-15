using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces;

/// <summary>
/// Defines the contract for Reminder service operations.
/// </summary>
public interface IReminderService
{
    /// <summary>
    /// Retrieves a Reminder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder.</param>
    /// <returns>The ReminderDto if found; otherwise, null.</returns>
    Task<ReminderDto?> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all Reminders.
    /// </summary>
    /// <returns>A collection of all ReminderDtos.</returns>
    Task<IEnumerable<ReminderDto>> GetAllAsync();

    /// <summary>
    /// Creates a new Reminder.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created ReminderDto.</returns>
    Task<ReminderDto> CreateAsync(CreateReminderDto createDto);

    /// <summary>
    /// Updates an existing Reminder.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    Task UpdateAsync(string id, UpdateReminderDto updateDto);

    /// <summary>
    /// Deletes a Reminder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder to delete.</param>
    Task DeleteAsync(string id);
}