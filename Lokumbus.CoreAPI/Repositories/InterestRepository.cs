using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IInterestRepository interface for Interest data access.
    /// </summary>
    public class InterestRepository : IInterestRepository
    {
        private readonly IMongoCollection<Interest> _interests;

        /// <summary>
        /// Initializes a new instance of the InterestRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public InterestRepository(IMongoDatabase database)
        {
            _interests = database.GetCollection<Interest>("Interests");
        }

        /// <inheritdoc />
        public async Task<Interest?> GetByIdAsync(string id)
        {
            return await _interests.Find(interest => interest.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Interest>> GetAllAsync()
        {
            return await _interests.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Interest interest)
        {
            await _interests.InsertOneAsync(interest);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Interest interest)
        {
            await _interests.ReplaceOneAsync(i => i.Id == interest.Id, interest);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _interests.DeleteOneAsync(i => i.Id == id);
        }
    }
}