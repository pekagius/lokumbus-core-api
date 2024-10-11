using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models;

public class InterestRelation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string InterestId { get; set; }
    public Interest Interest { get; set; } = null!;
    public string RelatedInterestId { get; set; }
    public Interest RelatedInterest { get; set; } = null!;
    public string? Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
}