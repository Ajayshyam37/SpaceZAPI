using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpaceZAPI.Models
{
    public class Telemetry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string sourceid { get; set; }

        [BsonElement("sourcetype")]
        public SourceType sourcetype { get; set; }

        [BsonElement("altitude")]
        public string altitude { get; set; } = String.Empty;

        [BsonElement("longitude")]
        public string longitude { get; set; } = String.Empty;

        [BsonElement("latitude")]
        public string latitude { get; set; } = String.Empty;

        [BsonElement("temparature")]
        public string temperature { get; set; } = String.Empty;

        [BsonElement("timetoOrbit")]
        public double timeToOrbit { get; set; }

        [BsonElement("createdat")]
        public DateTime createdat { get; set; }
    }

    public enum SourceType
    {
        LaunchVehicle,
        Payload
    }
}

