using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces;

/// <summary>
/// Defines the contract for Persona repository operations.
/// </summary>
public interface IPersonaRepository
{
    /// <summary>
    /// Retrieves a Persona by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona.</param>
    /// <returns>The Persona if found; otherwise, null.</returns>
    Task<Persona> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all Personas associated with a specific AppUser.
    /// </summary>
    /// <param name="userId">The unique identifier of the AppUser.</param>
    /// <returns>A collection of Personas associated with the AppUser.</returns>
    Task<IEnumerable<Persona>> GetByUserIdAsync(string userId);

    /// <summary>
    /// Retrieves all Personas.
    /// </summary>
    /// <returns>A collection of all Personas.</returns>
    Task<IEnumerable<Persona>> GetAllAsync();

    /// <summary>
    /// Creates a new Persona.
    /// </summary>
    /// <param name="persona">The Persona to create.</param>
    Task CreateAsync(Persona persona);

    /// <summary>
    /// Updates an existing Persona.
    /// </summary>
    /// <param name="persona">The Persona with updated information.</param>
    Task UpdateAsync(Persona persona);

    /// <summary>
    /// Deletes a Persona by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona to delete.</param>
    Task DeleteAsync(string id);
}