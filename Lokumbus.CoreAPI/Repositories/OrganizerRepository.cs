using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IOrganizerRepository interface for Organizer data access.
    /// </summary>
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly IMongoCollection<Organizer> _organizers;

        /// <summary>
        /// Initializes a new instance of the OrganizerRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public OrganizerRepository(IMongoDatabase database)
        {
            _organizers = database.GetCollection<Organizer>("Organizers");
        }

        /// <inheritdoc />
        public async Task<Organizer> GetByIdAsync(string id)
        {
            return await _organizers.Find(org => org.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Organizer>> GetAllAsync()
        {
            return await _organizers.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Organizer organizer)
        {
            await _organizers.InsertOneAsync(organizer);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Organizer organizer)
        {
            await _organizers.ReplaceOneAsync(org => org.Id == organizer.Id, organizer);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _organizers.DeleteOneAsync(org => org.Id == id);
        }
    }
}