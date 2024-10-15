using MongoDB.Driver;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;

namespace Lokumbus.CoreAPI.Repositories;

/// <summary>
/// Implementiert das <see cref="IReviewRepository"/>-Interface für den Datenzugriff von Reviews.
/// </summary>
public class ReviewRepository : IReviewRepository
{
    private readonly IMongoCollection<Review> _reviews;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="ReviewRepository"/>-Klasse.
    /// </summary>
    /// <param name="database">Die MongoDB-Datenbankinstanz.</param>
    public ReviewRepository(IMongoDatabase database)
    {
        _reviews = database.GetCollection<Review>("Reviews");
    }

    /// <inheritdoc />
    public async Task<Review?> GetByIdAsync(string id)
    {
        return await _reviews.Find(review => review.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _reviews.Find(_ => true).ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreateAsync(Review review)
    {
        await _reviews.InsertOneAsync(review);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Review review)
    {
        await _reviews.ReplaceOneAsync(r => r.Id == review.Id, review);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _reviews.DeleteOneAsync(review => review.Id == id);
    }
}