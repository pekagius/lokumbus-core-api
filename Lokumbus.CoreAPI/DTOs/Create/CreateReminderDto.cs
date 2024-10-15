using JetBrains.Annotations;
using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Create;

/// <summary>
/// Data Transfer Object for creating a new Reminder.
/// </summary>
[UsedImplicitly]
public class CreateReminderDto
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