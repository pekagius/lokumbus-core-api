using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implementiert das IInterestRelationService für die Geschäftslogik von InterestRelation.
    /// </summary>
    public class InterestRelationService : IInterestRelationService
    {
        private readonly IInterestRelationRepository _interestRelationRepository;
        private readonly TypeAdapterConfig _mapConfig;

        /// <summary>
        /// Initialisiert eine neue Instanz der InterestRelationService Klasse.
        /// </summary>
        /// <param name="interestRelationRepository">Das InterestRelationRepository.</param>
        /// <param name="mapConfig">Die Mapster-Konfiguration.</param>
        public InterestRelationService(IInterestRelationRepository interestRelationRepository, TypeAdapterConfig mapConfig)
        {
            _interestRelationRepository = interestRelationRepository;
            _mapConfig = mapConfig;
        }

        /// <inheritdoc />
        public async Task<InterestRelationDto?> GetByIdAsync(string id)
        {
            var interestRelation = await _interestRelationRepository.GetByIdAsync(id);
            return interestRelation?.Adapt<InterestRelationDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<InterestRelationDto>> GetAllAsync()
        {
            var interestRelations = await _interestRelationRepository.GetAllAsync();
            return interestRelations.Adapt<IEnumerable<InterestRelationDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<InterestRelationDto> CreateAsync(CreateInterestRelationDto createDto)
        {
            var interestRelation = createDto.Adapt<InterestRelation>(_mapConfig);
            interestRelation.CreatedAt = DateTime.UtcNow;
            interestRelation.IsActive = true;

            await _interestRelationRepository.CreateAsync(interestRelation);
            return interestRelation.Adapt<InterestRelationDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateInterestRelationDto updateDto)
        {
            var existingInterestRelation = await _interestRelationRepository.GetByIdAsync(id);
            if (existingInterestRelation == null)
            {
                throw new KeyNotFoundException($"InterestRelation mit ID {id} wurde nicht gefunden.");
            }

            updateDto.Adapt(existingInterestRelation, _mapConfig);
            existingInterestRelation.UpdatedAt = DateTime.UtcNow;

            await _interestRelationRepository.UpdateAsync(existingInterestRelation);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            var existingInterestRelation = await _interestRelationRepository.GetByIdAsync(id);
            if (existingInterestRelation == null)
            {
                throw new KeyNotFoundException($"InterestRelation mit ID {id} wurde nicht gefunden.");
            }

            await _interestRelationRepository.DeleteAsync(id);
        }
    }
}