using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a CalendarEventAttendee.
    /// </summary>
    public class CalendarEventAttendeeDto
    {
        /// <summary>
        /// The unique identifier of the CalendarEventAttendee.
        /// </summary>
        public string Id { get; set; }

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