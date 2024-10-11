using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the ICalendarEventAttendeeRepository interface for CalendarEventAttendee data access.
    /// </summary>
    public class CalendarEventAttendeeRepository : ICalendarEventAttendeeRepository
    {
        private readonly IMongoCollection<CalendarEventAttendee> _attendees;

        /// <summary>
        /// Initializes a new instance of the CalendarEventAttendeeRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public CalendarEventAttendeeRepository(IMongoDatabase database)
        {
            _attendees = database.GetCollection<CalendarEventAttendee>("CalendarEventAttendees");
        }

        /// <inheritdoc />
        public async Task<CalendarEventAttendee?> GetByIdAsync(string id)
        {
            return await _attendees.Find(attendee => attendee.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CalendarEventAttendee>> GetAllAsync()
        {
            return await _attendees.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(CalendarEventAttendee attendee)
        {
            await _attendees.InsertOneAsync(attendee);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(CalendarEventAttendee attendee)
        {
            await _attendees.ReplaceOneAsync(a => a.Id == attendee.Id, attendee);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _attendees.DeleteOneAsync(a => a.Id == id);
        }
    }
}