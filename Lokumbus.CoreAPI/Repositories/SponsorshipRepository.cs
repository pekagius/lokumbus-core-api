using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the ISponsorshipRepository interface for Sponsorship data access.
    /// </summary>
    public class SponsorshipRepository : ISponsorshipRepository
    {
        private readonly IMongoCollection<Sponsorship> _sponsorships;

        /// <summary>
        /// Initializes a new instance of the SponsorshipRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public SponsorshipRepository(IMongoDatabase database)
        {
            _sponsorships = database.GetCollection<Sponsorship>("Sponsorships");
        }

        /// <inheritdoc />
        public async Task<Sponsorship?> GetByIdAsync(string id)
        {
            return await _sponsorships.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Sponsorship>> GetAllAsync()
        {
            return await _sponsorships.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Sponsorship sponsorship)
        {
            await _sponsorships.InsertOneAsync(sponsorship);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Sponsorship sponsorship)
        {
            await _sponsorships.ReplaceOneAsync(s => s.Id == sponsorship.Id, sponsorship);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _sponsorships.DeleteOneAsync(s => s.Id == id);
        }
    }
}