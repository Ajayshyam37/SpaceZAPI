using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SpaceZAPI.Models
{

    public class SpaceCraft
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int spaceCraft_ID { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("state")]
        public State state { get; set; }
    }

    public enum State
    {
        Waiting,
        Active,
        DeOrbited
    }
}


