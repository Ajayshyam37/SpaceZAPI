using System;
using SpaceZAPI.Models;
using MongoDB.Driver;

namespace SpaceZAPI.Services
{
    public class SpaceCraftService : ISpaceCraftService
    {
        private readonly IMongoCollection<SpaceCraft> _spacecrafts;

        public SpaceCraftService(ISpaceZDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _spacecrafts = database.GetCollection<SpaceCraft>(settings.SpaceCraftsCollectionName);
        }
        public SpaceCraft Create(SpaceCraft spaceCraft)
        {
            _spacecrafts.InsertOne(spaceCraft);
            return spaceCraft;
        }

        public List<SpaceCraft> Get()
        {
            return _spacecrafts.Find(spacecraft => true).ToList();
        }

        public SpaceCraft Get(string id)
        {
            return _spacecrafts.Find(spacecraft => spacecraft.spaceCraft_ID == id).FirstOrDefault<SpaceCraft>();
        }

        public void Update(string id, SpaceCraft spaceCraft)
        {
            var updateDefinition = Builders<SpaceCraft>.Update
                .Set(s => s.state, spaceCraft.state)
                .Set(s => s.totalTimeToOrbit, spaceCraft.totalTimeToOrbit)
                .Set(s => s.spaceCraftTelemetery, spaceCraft.spaceCraftTelemetery);
            _spacecrafts.UpdateOne(s => s.spaceCraft_ID == id, updateDefinition);
        }

        public void UpdateSpaceCraft(string id, SpaceCraft spaceCraft)
        {
            var updateDefinition = Builders<SpaceCraft>.Update
                .Set(s => s.state, spaceCraft.state)
                .Set(s => s.name, spaceCraft.name)
                .Set(s => s.spaceCraftTelemetery, spaceCraft.spaceCraftTelemetery)
                .Set(s => s.orbitRadius, spaceCraft.orbitRadius)
                .Set(s => s.payloadid, spaceCraft.payloadid)
                .Set(s => s.payloadname, spaceCraft.payloadname)
                .Set(s => s.totalTimeToOrbit, spaceCraft.totalTimeToOrbit)
                .Set(s => s.launchtime, spaceCraft.launchtime);

            _spacecrafts.UpdateOne(s => s.spaceCraft_ID == id, updateDefinition);
        }

        public void UpdateTimeToOrbit(string id, SpaceCraft spaceCraft)
        {
            var updateDefinition = Builders<SpaceCraft>.Update
                .Set(s => s.totalTimeToOrbit, spaceCraft.totalTimeToOrbit);

            _spacecrafts.UpdateOne(s => s.spaceCraft_ID == id, updateDefinition);
        }


        public void Remove(string id)
        {
            _spacecrafts.DeleteOne(SpaceCraft => SpaceCraft.spaceCraft_ID == id);
        }
    }
}

