using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IEventRepository interface for Event data access.
    /// </summary>
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _events;

        /// <summary>
        /// Initializes a new instance of the EventRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public EventRepository(IMongoDatabase database)
        {
            _events = database.GetCollection<Event>("Events");
        }

        /// <inheritdoc />
        public async Task<Event> GetByIdAsync(string id)
        {
            return await _events.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _events.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Event @event)
        {
            await _events.InsertOneAsync(@event);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Event @event)
        {
            await _events.ReplaceOneAsync(e => e.Id == @event.Id, @event);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _events.DeleteOneAsync(e => e.Id == id);
        }
    }
}