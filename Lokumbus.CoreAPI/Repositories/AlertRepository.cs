using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IAlertRepository interface for Alert data access.
    /// </summary>
    public class AlertRepository : IAlertRepository
    {
        private readonly IMongoCollection<Alert> _alerts;

        /// <summary>
        /// Initializes a new instance of the AlertRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public AlertRepository(IMongoDatabase database)
        {
            _alerts = database.GetCollection<Alert>("Alerts");
        }

        /// <inheritdoc />
        public async Task<Alert?> GetByIdAsync(string id)
        {
            return await _alerts.Find(alert => alert.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Alert>> GetAllAsync()
        {
            return await _alerts.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Alert alert)
        {
            await _alerts.InsertOneAsync(alert);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Alert alert)
        {
            await _alerts.ReplaceOneAsync(a => a.Id == alert.Id, alert);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _alerts.DeleteOneAsync(alert => alert.Id == id);
        }
    }
}