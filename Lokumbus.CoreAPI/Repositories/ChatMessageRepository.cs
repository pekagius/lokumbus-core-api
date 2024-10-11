using Lokumbus.CoreAPI.Models.SubClasses;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the <see cref="IChatMessageRepository"/> interface for ChatMessage data access.
    /// </summary>
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly IMongoCollection<ChatMessage> _chatMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageRepository"/> class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public ChatMessageRepository(IMongoDatabase database)
        {
            _chatMessages = database.GetCollection<ChatMessage>("ChatMessages");
        }

        /// <inheritdoc />
        public async Task<ChatMessage> GetByIdAsync(string id)
        {
            return await _chatMessages.Find(message => message.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ChatMessage>> GetAllAsync()
        {
            return await _chatMessages.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(ChatMessage chatMessage)
        {
            await _chatMessages.InsertOneAsync(chatMessage);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(ChatMessage chatMessage)
        {
            await _chatMessages.ReplaceOneAsync(m => m.Id == chatMessage.Id, chatMessage);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _chatMessages.DeleteOneAsync(m => m.Id == id);
        }
    }
}