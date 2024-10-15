using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Ticket repository operations.
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// Retrieves a Ticket by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket.</param>
        /// <returns>The Ticket if found; otherwise, null.</returns>
        Task<Ticket?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Tickets.
        /// </summary>
        /// <returns>A collection of all Tickets.</returns>
        Task<IEnumerable<Ticket>> GetAllAsync();

        /// <summary>
        /// Creates a new Ticket.
        /// </summary>
        /// <param name="ticket">The Ticket to create.</param>
        Task CreateAsync(Ticket ticket);

        /// <summary>
        /// Updates an existing Ticket.
        /// </summary>
        /// <param name="ticket">The Ticket with updated information.</param>
        Task UpdateAsync(Ticket ticket);

        /// <summary>
        /// Deletes a Ticket by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket to delete.</param>
        Task DeleteAsync(string id);
    }
}