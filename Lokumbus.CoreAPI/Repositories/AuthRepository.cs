using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IAuthRepository interface for Auth data access.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<Auth> _auths;

        /// <summary>
        /// Initializes a new instance of the AuthRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public AuthRepository(IMongoDatabase database)
        {
            _auths = database.GetCollection<Auth>("Auths");
        }

        /// <inheritdoc />
        public async Task<Auth?> GetByIdAsync(string id)
        {
            return await _auths.Find(auth => auth.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Auth>> GetAllAsync()
        {
            return await _auths.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Auth auth)
        {
            await _auths.InsertOneAsync(auth);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Auth auth)
        {
            await _auths.ReplaceOneAsync(a => a.Id == auth.Id, auth);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _auths.DeleteOneAsync(auth => auth.Id == id);
        }
    }
}