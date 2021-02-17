using HseAr.Data;
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

namespace HseAr.DataAccess.Mongodb.SceneModificationHandlers
{
    public class DeleteObjectFromSceneHandler : ISceneModificationHandler
    {
        private readonly IMongoCollection<BsonDocument> _scenes;
        private readonly IMapper<SceneModification, SceneModificationBson> _sceneModMapper;

        public DeleteObjectFromSceneHandler(
            IMapper<SceneModification, SceneModificationBson> sceneModMapper,
            MongoContext mongoContext)
        {
            _scenes = mongoContext.ScenesAsBsonDocument;
            _sceneModMapper = sceneModMapper;
        }

        public DeleteObjectFromSceneHandler()
        {
        }

        public async Task<UpdateResult> Modify(SceneModification sceneMod)
        {
            var filter = SceneFilter.GetFilterById(sceneMod.ModelId);
            var sceneAsBson = await _scenes.Find(filter).FirstOrDefaultAsync();

            var uuid = sceneMod.Object["uuid"]?.ToString();

            DeleteObject(ref sceneAsBson, uuid);

            return await _scenes.UpdateOneAsync(filter, new BsonDocument("$set", sceneAsBson));
        }

        private void DeleteGeometry(ref BsonDocument scene, string geometryUuid)
        {
            var geometries = scene["geometries"].AsBsonArray;
            var geometryToDelete = geometries.FirstOrDefault(x => x["uuid"] == geometryUuid);
            geometries.Remove(geometryToDelete);
            scene["geometries"] = geometries;
        }

        private void DeleteMaterial(ref BsonDocument scene, string materialUuid)
        {
            var materials = scene["materials"].AsBsonArray;
            var materialToDelete = materials.FirstOrDefault(x => x["uuid"] == materialUuid);
            materials.Remove(materialToDelete);
            scene["materials"] = materials;
        }

        private void DeleteObject(ref BsonDocument scene, string objectUuid)
        {
            var objects = scene["object"]["children"].AsBsonArray;
            var objectToDelete = objects.FirstOrDefault(x => x["uuid"] == objectUuid);

            if(objectToDelete["material"] != null)
            {
                DeleteMaterial(ref scene, objectToDelete["material"].ToString());
            }

            if(objectToDelete["geometry"] != null)
            {
                DeleteGeometry(ref scene, objectToDelete["geometry"].ToString());
            }
            
            objects.Remove(objectToDelete);
            scene["object"]["children"] = objects;
        }

        public bool CatchTypeMatch(string modificationName)
        {
            return modificationName == "DeleteObjectFromScene";
        }
    }
}
