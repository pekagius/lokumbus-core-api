using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new CalendarEventAttendee.
    /// </summary>
    public class CreateCalendarEventAttendeeDto
    {
        /// <summary>
        /// The name of the attendee.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The email address of the attendee.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The status of the attendee.
        /// </summary>
        public AttendeeStatus Status { get; set; }
    }
}