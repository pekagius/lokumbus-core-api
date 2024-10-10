using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models.ValueObjects
{
    public class Attachment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public byte[]? Content { get; set; }
        public string? Url { get; set; }
    }
}