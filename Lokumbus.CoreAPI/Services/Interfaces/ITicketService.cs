using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Ticket service operations.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Retrieves a Ticket by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket.</param>
        /// <returns>The TicketDto if found; otherwise, null.</returns>
        Task<TicketDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Tickets.
        /// </summary>
        /// <returns>A collection of all TicketDtos.</returns>
        Task<IEnumerable<TicketDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Ticket.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created TicketDto.</returns>
        Task<TicketDto> CreateAsync(CreateTicketDto createDto);

        /// <summary>
        /// Updates an existing Ticket.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateTicketDto updateDto);

        /// <summary>
        /// Deletes a Ticket by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket to delete.</param>
        Task DeleteAsync(string id);
    }
}