using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SpaceZAPI.Models
{ 
    public class PayLoad
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string payloadid { get; set; }

        [BsonElement("payloadname")]
        public string payloadname { get; set; }

        [BsonElement("payloadstate")]
        public State payloadstate { get; set; }

        [BsonElement("payloadttype")]
        public PayloadType payLoadType { get; set; }

        [BsonElement("payloaddata")]
        public Boolean payLoadData { get; set; }
    }

    public enum PayloadType
    {
        Communication,
        Spy,
        Scientific
    }
}