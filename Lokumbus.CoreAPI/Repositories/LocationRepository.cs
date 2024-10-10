using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories;

/// <summary>
/// Implements the <see cref="ILocationRepository"/> interface for Location data operations.
/// </summary>
public class LocationRepository : ILocationRepository
{
    private readonly IMongoCollection<Location> _locations;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationRepository"/> class.
    /// </summary>
    /// <param name="database"></param>
    public LocationRepository(IMongoDatabase database)
    {
        _locations = database.GetCollection<Location>("Locations");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _locations.Find(location => true).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Location?> GetByIdAsync(string id)
    {
        return await _locations.Find<Location>(location => location.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<Location> CreateAsync(Location location)
    {
        await _locations.InsertOneAsync(location);
        return location;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Location location)
    {
        await _locations.ReplaceOneAsync(l => l.Id == location.Id, location);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _locations.DeleteOneAsync(location => location.Id == id);
    }
}