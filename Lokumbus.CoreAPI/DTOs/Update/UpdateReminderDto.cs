using JetBrains.Annotations;

namespace Lokumbus.CoreAPI.DTOs.Update;

/// <summary>
/// Data Transfer Object for updating an existing Reminder.
/// </summary>
[UsedImplicitly]
public class UpdateReminderDto
{
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
}