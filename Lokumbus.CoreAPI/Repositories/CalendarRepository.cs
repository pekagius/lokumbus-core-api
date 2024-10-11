using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implementiert das ICalendarRepository-Interface für den Zugriff auf Kalenderdaten.
    /// </summary>
    public class CalendarRepository : ICalendarRepository
    {
        private readonly IMongoCollection<Calendar> _calendars;
    
        /// <summary>
        /// Initialisiert eine neue Instanz der CalendarRepository-Klasse.
        /// </summary>
        /// <param name="database">Die MongoDB-Datenbankinstanz.</param>
        public CalendarRepository(IMongoDatabase database)
        {
            _calendars = database.GetCollection<Calendar>("Calendars");
        }
    
        /// <inheritdoc />
        public async Task<Calendar?> GetByIdAsync(string id)
        {
            return await _calendars.Find(calendar => calendar.Id == id).FirstOrDefaultAsync();
        }
    
        /// <inheritdoc />
        public async Task<IEnumerable<Calendar>> GetAllAsync()
        {
            return await _calendars.Find(_ => true).ToListAsync();
        }
    
        /// <inheritdoc />
        public async Task CreateAsync(Calendar calendar)
        {
            await _calendars.InsertOneAsync(calendar);
        }
    
        /// <inheritdoc />
        public async Task UpdateAsync(Calendar calendar)
        {
            await _calendars.ReplaceOneAsync(c => c.Id == calendar.Id, calendar);
        }
    
        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _calendars.DeleteOneAsync(calendar => calendar.Id == id);
        }
    }
}