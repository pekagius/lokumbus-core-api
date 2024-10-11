using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implementiert das IInterestRelationRepository für den Datenzugriff auf InterestRelation.
    /// </summary>
    public class InterestRelationRepository : IInterestRelationRepository
    {
        private readonly IMongoCollection<InterestRelation> _interestRelations;

        /// <summary>
        /// Initialisiert eine neue Instanz der InterestRelationRepository Klasse.
        /// </summary>
        /// <param name="database">Die MongoDB Datenbankinstanz.</param>
        public InterestRelationRepository(IMongoDatabase database)
        {
            _interestRelations = database.GetCollection<InterestRelation>("InterestRelations");
        }

        /// <inheritdoc />
        public async Task<InterestRelation?> GetByIdAsync(string id)
        {
            return await _interestRelations.Find(ir => ir.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<InterestRelation>> GetAllAsync()
        {
            return await _interestRelations.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(InterestRelation interestRelation)
        {
            await _interestRelations.InsertOneAsync(interestRelation);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(InterestRelation interestRelation)
        {
            await _interestRelations.ReplaceOneAsync(ir => ir.Id == interestRelation.Id, interestRelation);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _interestRelations.DeleteOneAsync(ir => ir.Id == id);
        }
    }
}