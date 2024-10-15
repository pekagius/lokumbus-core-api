using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Sponsorship service operations.
    /// </summary>
    public interface ISponsorshipService
    {
        /// <summary>
        /// Retrieves a Sponsorship by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship.</param>
        /// <returns>The SponsorshipDto if found; otherwise, null.</returns>
        Task<SponsorshipDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Sponsorships.
        /// </summary>
        /// <returns>A collection of all SponsorshipDtos.</returns>
        Task<IEnumerable<SponsorshipDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Sponsorship.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created SponsorshipDto.</returns>
        Task<SponsorshipDto> CreateAsync(CreateSponsorshipDto createDto);

        /// <summary>
        /// Updates an existing Sponsorship.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateSponsorshipDto updateDto);

        /// <summary>
        /// Deletes a Sponsorship by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship to delete.</param>
        Task DeleteAsync(string id);
    }
}