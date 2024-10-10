using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces;

public interface IAreaRepository
{
    Task<Area> GetByIdAsync(string id);
    Task<IEnumerable<Area>> GetAllAsync();
    Task CreateAsync(Area area);
    Task UpdateAsync(Area area);
    Task DeleteAsync(string id);
}