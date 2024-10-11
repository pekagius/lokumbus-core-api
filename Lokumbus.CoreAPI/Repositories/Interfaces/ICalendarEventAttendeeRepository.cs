using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for CalendarEventAttendee repository operations.
    /// </summary>
    public interface ICalendarEventAttendeeRepository
    {
        /// <summary>
        /// Retrieves a CalendarEventAttendee by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee.</param>
        /// <returns>The CalendarEventAttendee if found; otherwise, null.</returns>
        Task<CalendarEventAttendee?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all CalendarEventAttendees.
        /// </summary>
        /// <returns>A collection of all CalendarEventAttendees.</returns>
        Task<IEnumerable<CalendarEventAttendee>> GetAllAsync();

        /// <summary>
        /// Creates a new CalendarEventAttendee.
        /// </summary>
        /// <param name="attendee">The CalendarEventAttendee to create.</param>
        Task CreateAsync(CalendarEventAttendee attendee);

        /// <summary>
        /// Updates an existing CalendarEventAttendee.
        /// </summary>
        /// <param name="attendee">The CalendarEventAttendee with updated information.</param>
        Task UpdateAsync(CalendarEventAttendee attendee);

        /// <summary>
        /// Deletes a CalendarEventAttendee by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee to delete.</param>
        Task DeleteAsync(string id);
    }
}