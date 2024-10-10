using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces;

public interface IAreaService
{
    Task<AreaDto> GetByIdAsync(string id);
    Task<IEnumerable<AreaDto>> GetAllAsync();
    Task<AreaDto> CreateAsync(CreateAreaDto createAreaDto);
    Task<AreaDto> UpdateAsync(string id, UpdateAreaDto updateAreaDto);
    Task DeleteAsync(string id);
}