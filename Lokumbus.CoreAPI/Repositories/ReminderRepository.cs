using MongoDB.Driver;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;

namespace Lokumbus.CoreAPI.Repositories;

/// <summary>
/// Implements the <see cref="IReminderRepository"/> interface for Reminder data access.
/// </summary>
public class ReminderRepository : IReminderRepository
{
    private readonly IMongoCollection<Reminder> _reminders;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReminderRepository"/> class.
    /// </summary>
    /// <param name="database">The MongoDB database instance.</param>
    public ReminderRepository(IMongoDatabase database)
    {
        _reminders = database.GetCollection<Reminder>("Reminders");
    }

    /// <inheritdoc />
    public async Task<Reminder?> GetByIdAsync(string id)
    {
        return await _reminders.Find(reminder => reminder.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Reminder>> GetAllAsync()
    {
        return await _reminders.Find(_ => true).ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreateAsync(Reminder reminder)
    {
        await _reminders.InsertOneAsync(reminder);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Reminder reminder)
    {
        await _reminders.ReplaceOneAsync(r => r.Id == reminder.Id, reminder);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _reminders.DeleteOneAsync(reminder => reminder.Id == id);
    }
}