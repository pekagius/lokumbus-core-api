using Lokumbus.CoreAPI.Models.SubClasses;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the <see cref="IAlertMessageRepository"/> interface for AlertMessage data access.
    /// </summary>
    public class AlertMessageRepository : IAlertMessageRepository
    {
        private readonly IMongoCollection<AlertMessage> _alertMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertMessageRepository"/> class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public AlertMessageRepository(IMongoDatabase database)
        {
            _alertMessages = database.GetCollection<AlertMessage>("AlertMessages");
        }

        /// <inheritdoc />
        public async Task<AlertMessage> GetByIdAsync(string id)
        {
            return await _alertMessages.Find(alert => alert.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AlertMessage>> GetAllAsync()
        {
            return await _alertMessages.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(AlertMessage alertMessage)
        {
            await _alertMessages.InsertOneAsync(alertMessage);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(AlertMessage alertMessage)
        {
            await _alertMessages.ReplaceOneAsync(a => a.Id == alertMessage.Id, alertMessage);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _alertMessages.DeleteOneAsync(a => a.Id == id);
        }
    }
}