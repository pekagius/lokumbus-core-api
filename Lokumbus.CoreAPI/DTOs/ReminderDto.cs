using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs;

/// <summary>
/// Data Transfer Object representing a Reminder.
/// </summary>
public class ReminderDto
{
    /// <summary>
    /// The unique identifier of the Reminder.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The number of minutes before the event to trigger the reminder.
    /// </summary>
    public int? Minutes { get; set; }

    /// <summary>
    /// The number of hours before the event to trigger the reminder.
    /// </summary>
    public int? Hours { get; set; }

    /// <summary>
    /// The number of days before the event to trigger the reminder.
    /// </summary>
    public int? Days { get; set; }

    /// <summary>
    /// The date and time when the reminder was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the reminder was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the reminder is active.
    /// </summary>
    public bool? IsActive { get; set; }
}