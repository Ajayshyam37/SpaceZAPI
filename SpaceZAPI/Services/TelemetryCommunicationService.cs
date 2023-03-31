using System;
using SpaceZAPI.Models;
using MongoDB.Driver;

namespace SpaceZAPI.Services
{
    public class TelemetryCommunicationService : ITelemetryCommunicationService
    {
        private readonly IMongoCollection<Telemetry> _telemetry;

        public TelemetryCommunicationService(ISpaceZDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _telemetry = database.GetCollection<Telemetry>(settings.TelemetryCommunicationCollectionName);
        }

        public Telemetry Create(Telemetry telemetry)
        {
            _telemetry.InsertOne(telemetry);
            return telemetry;
        }

        public List<Telemetry> Get(string id)
        {
            return _telemetry.Find(telemetry => telemetry.sourceid == id).ToList();
        }

        public Telemetry GetTelemetry(string id)
        {
            return _telemetry.Find(telemetry => telemetry.id == id).FirstOrDefault<Telemetry>();
        }

        public void Remove(string id)
        {
            _telemetry.DeleteMany(telemetry => telemetry.sourceid == id);
        }

        public void Update(string id, Telemetry telemetry)
        {
            
        }
    }
}

