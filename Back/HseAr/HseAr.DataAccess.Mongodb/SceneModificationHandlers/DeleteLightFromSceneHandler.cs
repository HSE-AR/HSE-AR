using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.Mongodb.Filters;
using HseAr.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HseAr.Data.Enums;

namespace HseAr.DataAccess.Mongodb.SceneModificationHandlers
{
    public class DeleteLightFromSceneHandler : ISceneModificationHandler
    {
        private readonly IMongoCollection<BsonDocument> _scenes;
        private readonly IMapper<SceneModification, SceneModificationBson> _sceneModMapper;

        public DeleteLightFromSceneHandler(
            IMapper<SceneModification, SceneModificationBson> sceneModMapper,
            MongoContext mongoContext)
        {
            _scenes = mongoContext.ScenesAsBsonDocument;
            _sceneModMapper = sceneModMapper;
        }
        
        public bool CatchTypeMatch(SceneModificationType modificationType)
        {
            return modificationType == SceneModificationType.DeleteLightFromScene;
        }

        public async Task<UpdateResult> Modify(SceneModification sceneMod)
        {
            var filter = SceneFilter.GetFilterById(sceneMod.ModelId);
            var sceneAsBson = await _scenes.Find(filter).FirstOrDefaultAsync();

            var uuid = sceneMod.DataJson["uuid"]?.ToString();

            DeleteLight(ref sceneAsBson, uuid);

            return await _scenes.UpdateOneAsync(filter, new BsonDocument("$set", sceneAsBson));
        }
        
        private void DeleteLight(ref BsonDocument scene, string lightUuid)
        {
            var objects = scene["object"]["children"].AsBsonArray;
            var objectToDelete = objects.FirstOrDefault(x => x["uuid"] == lightUuid);


            objects.Remove(objectToDelete);
            scene["object"]["children"] = objects;
        }
    }
}
