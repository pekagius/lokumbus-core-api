using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IActivityRepository interface für Activity data access.
    /// </summary>
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoCollection<Activity> _activities;

        /// <summary>
        /// Initializes a new instance of the ActivityRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public ActivityRepository(IMongoDatabase database)
        {
            _activities = database.GetCollection<Activity>("Activities");
        }

        /// <inheritdoc />
        public async Task<Activity> GetByIdAsync(string id)
        {
            return await _activities.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _activities.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Activity activity)
        {
            await _activities.InsertOneAsync(activity);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Activity activity)
        {
            await _activities.ReplaceOneAsync(a => a.Id == activity.Id, activity);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _activities.DeleteOneAsync(a => a.Id == id);
        }
    }
}