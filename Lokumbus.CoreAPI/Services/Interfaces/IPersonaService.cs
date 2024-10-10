using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces;

/// <summary>
/// Defines the contract for Persona service operations.
/// </summary>
public interface IPersonaService
{
    /// <summary>
    /// Retrieves a Persona by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona.</param>
    /// <returns>The PersonaDto if found; otherwise, null.</returns>
    Task<PersonaDto> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all Personas associated with a specific AppUser.
    /// </summary>
    /// <param name="userId">The unique identifier of the AppUser.</param>
    /// <returns>A collection of PersonaDtos associated with the AppUser.</returns>
    Task<IEnumerable<PersonaDto>> GetByUserIdAsync(string userId);

    /// <summary>
    /// Retrieves all Personas.
    /// </summary>
    /// <returns>A collection of all PersonaDtos.</returns>
    Task<IEnumerable<PersonaDto>> GetAllAsync();

    /// <summary>
    /// Creates a new Persona.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created PersonaDto.</returns>
    Task<PersonaDto> CreateAsync(CreatePersonaDto createDto);

    /// <summary>
    /// Updates an existing Persona.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    Task UpdateAsync(string id, UpdatePersonaDto updateDto);

    /// <summary>
    /// Deletes a Persona by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona to delete.</param>
    Task DeleteAsync(string id);
}