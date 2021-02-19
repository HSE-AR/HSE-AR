using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.Mongodb.Filters;
using HseAr.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HseAr.DataAccess.Mongodb.SceneModificationHandlers
{
    public class AddLightToSceneHandler : ISceneModificationHandler
    {
        private readonly IMongoCollection<BsonDocument> _scenes;
        private readonly IMapper<SceneModification, SceneModificationBson> _sceneModMapper;

        public AddLightToSceneHandler(
            IMapper<SceneModification, SceneModificationBson> sceneModMapper,
            MongoContext mongoContext)
        {
            _scenes = mongoContext.ScenesAsBsonDocument;
            _sceneModMapper = sceneModMapper;
        }

        public AddLightToSceneHandler()
        {
        }

        public async Task<UpdateResult> Modify(SceneModification sceneMod)
        {
            var sceneModEntity = _sceneModMapper.Map(sceneMod);

            var update = Builders<BsonDocument>.Update
                .AddToSet("object.children", sceneModEntity.Object);

            var filter = SceneFilter.GetFilterById(sceneModEntity.ModelId);

            return await _scenes.UpdateOneAsync(filter, update);
        }

        public bool CatchTypeMatch(string modificationName)
        {
            return modificationName == "AddLightToScene";
        }


    }
}
