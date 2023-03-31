using System;
using SpaceZAPI.Models;
using MongoDB.Driver;

namespace SpaceZAPI.Services
{
    public class PayLoadService : IPayLoadService
    {
        private readonly IMongoCollection<PayLoad> _payload;

        public PayLoadService(ISpaceZDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _payload = database.GetCollection<PayLoad>(settings.PayLoadCollectionName);
        }
        public PayLoad Create(PayLoad payload)
        {
            _payload.InsertOne(payload);
            return payload;
        }

        public List<PayLoad> Get()
        {
            return _payload.Find(payload => true).ToList();
        }

        public PayLoad Get(string id)
        {
            return _payload.Find(payload => payload.payloadid == id).FirstOrDefault<PayLoad>();
        }

        public void UpdateState(string id, PayLoad payLoad)
        {
            var updateDefinition = Builders<PayLoad>.Update
                .Set(s => s.payloadstate, payLoad.payloadstate)
                .Set(s => s.payLoadData, payLoad.payLoadData);
            _payload.UpdateOne(s => s.payloadid == id, updateDefinition);
        }

        public void UpdatePayLoad(string id, PayLoad payLoad)
        {
            var updateDefinition = Builders<PayLoad>.Update
                 .Set(s => s.payloadname, payLoad.payloadname)
                 .Set(s => s.payLoadType, payLoad.payLoadType)
                .Set(s => s.payloadstate, payLoad.payloadstate)
                .Set(s => s.payLoadData, payLoad.payLoadData);
               
            _payload.UpdateOne(s => s.payloadid == id, updateDefinition);
        }

        public void Remove(string id)
        {
            _payload.DeleteOne(payLoad => payLoad.payloadid == id);
        }
    }
}

