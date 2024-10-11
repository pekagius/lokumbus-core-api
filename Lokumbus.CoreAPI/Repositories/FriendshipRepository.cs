using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implementiert das IFriendshipRepository-Interface für den Datenzugriff auf Friendship-Entitäten.
    /// </summary>
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly IMongoCollection<Friendship> _friendships;
    
        /// <summary>
        /// Initialisiert eine neue Instanz der FriendshipRepository-Klasse.
        /// </summary>
        /// <param name="database">Die MongoDB-Datenbankinstanz.</param>
        public FriendshipRepository(IMongoDatabase database)
        {
            _friendships = database.GetCollection<Friendship>("Friendships");
        }
    
        /// <inheritdoc />
        public async Task<Friendship?> GetByIdAsync(string id)
        {
            return await _friendships.Find(f => f.Id == id).FirstOrDefaultAsync();
        }
    
        /// <inheritdoc />
        public async Task<IEnumerable<Friendship>> GetAllAsync()
        {
            return await _friendships.Find(_ => true).ToListAsync();
        }
    
        /// <inheritdoc />
        public async Task<IEnumerable<Friendship>> GetByPersonaIdAsync(string personaId)
        {
            // Sucht Friendships, bei denen die PersonaId entweder die angegebene ist oder FriendPersonaId ist.
            var filter = Builders<Friendship>.Filter.Or(
                Builders<Friendship>.Filter.Eq(f => f.PersonaId, personaId),
                Builders<Friendship>.Filter.Eq(f => f.FriendPersonaId, personaId)
            );
            return await _friendships.Find(filter).ToListAsync();
        }
    
        /// <inheritdoc />
        public async Task CreateAsync(Friendship friendship)
        {
            await _friendships.InsertOneAsync(friendship);
        }
    
        /// <inheritdoc />
        public async Task UpdateAsync(Friendship friendship)
        {
            await _friendships.ReplaceOneAsync(f => f.Id == friendship.Id, friendship);
        }
    
        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _friendships.DeleteOneAsync(f => f.Id == id);
        }
    }
}