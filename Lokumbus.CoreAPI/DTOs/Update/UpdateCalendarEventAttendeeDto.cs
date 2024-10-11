using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object for updating an existing CalendarEventAttendee.
    /// </summary>
    public class UpdateCalendarEventAttendeeDto
    {
        /// <summary>
        /// The updated name of the attendee.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The updated email address of the attendee.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The updated status of the attendee.
        /// </summary>
        public AttendeeStatus? Status { get; set; }
    }
}