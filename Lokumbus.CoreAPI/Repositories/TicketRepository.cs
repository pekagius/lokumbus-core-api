using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the ITicketRepository interface for Ticket data access.
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _tickets;

        /// <summary>
        /// Initializes a new instance of the TicketRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public TicketRepository(IMongoDatabase database)
        {
            _tickets = database.GetCollection<Ticket>("Tickets");
        }

        /// <inheritdoc />
        public async Task<Ticket?> GetByIdAsync(string id)
        {
            return await _tickets.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _tickets.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Ticket ticket)
        {
            await _tickets.InsertOneAsync(ticket);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Ticket ticket)
        {
            await _tickets.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _tickets.DeleteOneAsync(t => t.Id == id);
        }
    }
}