using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories;

public class AreaRepository : IAreaRepository
{
    private readonly IMongoCollection<Area> _areas;

    public AreaRepository(IMongoDatabase database)
    {
        _areas = database.GetCollection<Area>("Areas");
    }

    public async Task<Area> GetByIdAsync(string id)
    {
        return await _areas.Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Area>> GetAllAsync()
    {
        return await _areas.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(Area area)
    {
        await _areas.InsertOneAsync(area);
    }

    public async Task UpdateAsync(Area area)
    {
        await _areas.ReplaceOneAsync(a => a.Id == area.Id, area);
    }

    public async Task DeleteAsync(string id)
    {
        await _areas.DeleteOneAsync(a => a.Id == id);
    }
}