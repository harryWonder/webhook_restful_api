using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using webhook_restful_api.Common.Enums;

namespace webhook_restful_api.Models
{
    public class WebhookHash
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [Required]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string Reference { get; set; }

        [Required]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string Hash { get; set; }

        [Required]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string IpAddress { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Status { get; set; } = (int) StatusEnum.Active;

        [Required]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime LastAccessed { get; set; }

        [BsonDefaultValue(BsonType.DateTime)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonDefaultValue(BsonType.DateTime)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
