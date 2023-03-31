using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SpaceZAPI.Models
{

    public class Communication
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int communication_ID { get; set; }

        [BsonElement("spaceCraft_ID")]
        public int spaceCraft_ID { get; set; }

        [BsonElement("payloadid")]
        public Guid payLoad_ID { get; set; }

        [BsonElement("payloadstate")]
        public State payloadState { get; set; }

        [BsonElement("payloadtype")]
        public PayloadType payloadType { get; set; }

        [BsonElement("telemetry")]
        public Boolean telemetry { get; set; }

        [BsonElement("payloaddata")]
        public Boolean payloadData { get; set; }

    }
}

