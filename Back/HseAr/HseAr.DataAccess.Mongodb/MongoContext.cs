using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb 
{
     public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly ModelsDatabaseSettings _settings;

        public MongoContext(Configuration configuration)
        {
            _settings = configuration.ModelsDatabaseSettings;

            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }
        
        public IMongoCollection<SceneBson> Scenes 
            => _database.GetCollection<SceneBson>(_settings.ScenesCollectionName);

        public IMongoCollection<BsonDocument> ScenesAsBsonDocument 
            => _database.GetCollection<BsonDocument>(_settings.ScenesCollectionName);
        
        public IMongoCollection<BsonDocument> ModificationsAsBsonDocument 
            => _database.GetCollection<BsonDocument>(_settings.ModificationsCollectionName);
        
    }
}