using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implementiert das IInviteRepository für den Datenzugriff auf Invite.
    /// </summary>
    public class InviteRepository : IInviteRepository
    {
        private readonly IMongoCollection<Invite> _invites;

        /// <summary>
        /// Initialisiert eine neue Instanz der InviteRepository Klasse.
        /// </summary>
        /// <param name="database">Die MongoDB Datenbankinstanz.</param>
        public InviteRepository(IMongoDatabase database)
        {
            _invites = database.GetCollection<Invite>("Invites");
        }

        /// <inheritdoc />
        public async Task<Invite?> GetByIdAsync(string id)
        {
            return await _invites.Find(invite => invite.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Invite>> GetAllAsync()
        {
            return await _invites.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Invite invite)
        {
            await _invites.InsertOneAsync(invite);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Invite invite)
        {
            await _invites.ReplaceOneAsync(i => i.Id == invite.Id, invite);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _invites.DeleteOneAsync(invite => invite.Id == id);
        }
    }
}