using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services;

public class AreaService : IAreaService
{
    private readonly IAreaRepository _areaRepository;

    public AreaService(IAreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    public async Task<AreaDto> GetByIdAsync(string id)
    {
        var area = await _areaRepository.GetByIdAsync(id);
        return area.Adapt<AreaDto>();
    }

    public async Task<IEnumerable<AreaDto>> GetAllAsync()
    {
        var areas = await _areaRepository.GetAllAsync();
        return areas.Adapt<IEnumerable<AreaDto>>();
    }

    public async Task<AreaDto> CreateAsync(CreateAreaDto createAreaDto)
    {
        var area = createAreaDto.Adapt<Area>();
        await _areaRepository.CreateAsync(area);
        return area.Adapt<AreaDto>();
    }

    public async Task<AreaDto> UpdateAsync(string id, UpdateAreaDto updateAreaDto)
    {
        var area = await _areaRepository.GetByIdAsync(id);
        updateAreaDto.Adapt(area);
        await _areaRepository.UpdateAsync(area);
        return area.Adapt<AreaDto>();
    }

    public async Task DeleteAsync(string id)
    {
        await _areaRepository.DeleteAsync(id);
    }
}