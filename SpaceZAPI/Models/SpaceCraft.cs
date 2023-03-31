using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SpaceZAPI.Models
{

    public class SpaceCraft
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string spaceCraft_ID { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("state")]
        public State state { get; set; }

        [BsonElement("spacecrafttelemetry")]
        public bool spaceCraftTelemetery { get; set; }

        [BsonElement("payloadname")]
        public string payloadname { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string payloadid { get; set; }

        [BsonElement("orbitradius")]
        public double orbitRadius { get; set; }

        [BsonElement("totalTimeOrbit")]
        public double totalTimeToOrbit { get; set; }

        [BsonElement("createdtime")]
        [BsonDateTimeOptions]
        public DateTime launchtime { get; set; }

        [BsonElement("launchtime")]
        [BsonDateTimeOptions]
        public DateTime createdtime { get; set; }

    }
    public enum State
    {
        Waiting,
        Active,
        DeOrbited
    }
}


