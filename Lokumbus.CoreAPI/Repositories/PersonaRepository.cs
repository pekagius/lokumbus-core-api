using MongoDB.Driver;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;

namespace Lokumbus.CoreAPI.Repositories;

/// <summary>
/// Implements the IPersonaRepository interface for Persona data access.
/// </summary>
public class PersonaRepository : IPersonaRepository
{
    private readonly IMongoCollection<Persona> _personas;

    /// <summary>
    /// Initializes a new instance of the PersonaRepository class.
    /// </summary>
    /// <param name="database">The MongoDB database instance.</param>
    public PersonaRepository(IMongoDatabase database)
    {
        _personas = database.GetCollection<Persona>("Personas");
    }

    /// <inheritdoc />
    public async Task<Persona> GetByIdAsync(string id)
    {
        return await _personas.Find(persona => persona.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Persona>> GetByUserIdAsync(string userId)
    {
        return await _personas.Find(persona => persona.UserId == userId).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _personas.Find(_ => true).ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreateAsync(Persona persona)
    {
        await _personas.InsertOneAsync(persona);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Persona persona)
    {
        await _personas.ReplaceOneAsync(p => p.Id == persona.Id, persona);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _personas.DeleteOneAsync(persona => persona.Id == id);
    }
}