using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SpaceZAPI.Models
{
    public enum PayloadType
    {
        Communication,
        Spy,
        Scientific 
    }
}