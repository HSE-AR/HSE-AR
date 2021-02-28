using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
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
    public class UpdateTransformHandler : ISceneModificationHandler
    {
        private readonly IMongoCollection<BsonDocument> _scenes;
        private readonly IMapper<SceneModification, SceneModificationBson> _sceneModMapper;

        public UpdateTransformHandler(
            IMapper<SceneModification, SceneModificationBson> sceneModMapper,
            MongoContext mongoContext)
        {
            _scenes = mongoContext.ScenesAsBsonDocument;
            _sceneModMapper = sceneModMapper;
        }

        public bool CatchTypeMatch(SceneModificationType modificationType)
        {
            return modificationType == SceneModificationType.UpdateTransform;
        }

        public async Task<UpdateResult> Modify(SceneModification sceneMod)
        {
            var sceneModEntity = _sceneModMapper.Map(sceneMod);
            var filter = SceneFilter.GetFilterById(sceneMod.SceneId);
            var sceneAsBson = await _scenes.Find(filter).FirstOrDefaultAsync();

            var uuid = sceneMod.DataJson["uuid"]?.ToString();

            var objects = sceneAsBson["object"]["children"].AsBsonArray;

            var isFound = FindAndUpdateElementRecursively(ref objects, ref sceneModEntity, uuid);

            if (!isFound)
            {
                throw new Exception("Element not found");
            }

            sceneAsBson["object"]["children"] = objects;

            return await _scenes.UpdateOneAsync(filter, new BsonDocument("$set", sceneAsBson));

        }

        private bool FindAndUpdateElementRecursively(ref BsonArray children, 
           ref SceneModificationBson sceneModEntity, string uuid)
        {
            bool isFoundAndUpdated = false;
            foreach (var obj in children)
            {
                if (obj["uuid"] == uuid)
                {
                    obj["matrix"] = sceneModEntity.DataBson["matrix"];
                    return true;
                }

                if (obj.AsBsonDocument.Contains("children"))
                {
                    var nested_obj = obj["children"].AsBsonArray;
                    isFoundAndUpdated = FindAndUpdateElementRecursively(ref nested_obj, ref sceneModEntity, uuid);
                }
                
                if (isFoundAndUpdated)
                {
                    break;
                }
            }
            return isFoundAndUpdated; 
        }
    }
}
