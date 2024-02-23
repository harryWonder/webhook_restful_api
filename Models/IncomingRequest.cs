using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webhook_restful_api.Models
{
    public class IncomingRequest
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [Required]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId WebhookHashId { get; set; }

        [Required]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string IpAddress { get; set; }

        [Required]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string RequestUrl { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public Headers Headers { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public object Query {  get; set; }

        [BsonRepresentation(BsonType.Document)]
        public object Params { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public object Body { get; set; }

        [BsonDefaultValue(BsonType.DateTime)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonDefaultValue(BsonType.DateTime)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }

    }

    public record Headers
    {
        public string Authorization { get; set; } = string.Empty;

        public string CacheControl { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public string ContentEncoding { get; set; } = string.Empty;

        public string ContentLanguage { get; set; } = string.Empty;
    }
}
