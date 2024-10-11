using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for CalendarEventAttendee service operations.
    /// </summary>
    public interface ICalendarEventAttendeeService
    {
        /// <summary>
        /// Retrieves a CalendarEventAttendee by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee.</param>
        /// <returns>The CalendarEventAttendeeDto if found; otherwise, null.</returns>
        Task<CalendarEventAttendeeDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all CalendarEventAttendees.
        /// </summary>
        /// <returns>A collection of all CalendarEventAttendeeDtos.</returns>
        Task<IEnumerable<CalendarEventAttendeeDto>> GetAllAsync();

        /// <summary>
        /// Creates a new CalendarEventAttendee.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created CalendarEventAttendeeDto.</returns>
        Task<CalendarEventAttendeeDto> CreateAsync(CreateCalendarEventAttendeeDto createDto);

        /// <summary>
        /// Updates an existing CalendarEventAttendee.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateCalendarEventAttendeeDto updateDto);

        /// <summary>
        /// Deletes a CalendarEventAttendee by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee to delete.</param>
        Task DeleteAsync(string id);
    }
}