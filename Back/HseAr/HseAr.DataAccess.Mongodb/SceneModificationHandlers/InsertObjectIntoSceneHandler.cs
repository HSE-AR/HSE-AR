using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.Mongodb.Filters;
using HseAr.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Enums;

namespace HseAr.DataAccess.Mongodb.SceneModificationHandlers
{
    public class InsertObjectIntoSceneHandler : ISceneModificationHandler
    {
        private readonly IMongoCollection<BsonDocument> _scenes;
        private readonly IMapper<SceneModification, SceneModificationBson> _sceneModMapper;

        public InsertObjectIntoSceneHandler(
            IMapper<SceneModification, SceneModificationBson> sceneModMapper,
            MongoContext mongoContext)
        {
            _scenes = mongoContext.ScenesAsBsonDocument;
            _sceneModMapper = sceneModMapper;
        }

        public bool CatchTypeMatch(SceneModificationType modificationType)
        {
            return modificationType == SceneModificationType.InsertObjectToScene;
        }

        public async Task<UpdateResult> Modify(SceneModification sceneMod)
        {
            var sceneModEntity = _sceneModMapper.Map(sceneMod);


            if (sceneModEntity.DataBson["material"] == null
                || sceneModEntity.DataBson["geometry"] == null
                || sceneModEntity.DataBson["object"] == null) //object или всё же objectChild?
            {
                throw new Exception("Для добавления объекта должны быть не нулевыми " +
                                    "поля Material, Geometry, ObjectChildren");
            }

            var update = Builders<BsonDocument>.Update
                .AddToSet("object.children", sceneModEntity.DataBson["object"])
                .AddToSet("materials", sceneModEntity.DataBson["material"])
                .AddToSet("geometries", sceneModEntity.DataBson["geometry"]);

            var filter = SceneFilter.GetFilterById(sceneModEntity.SceneId);

            return await _scenes.UpdateOneAsync(filter, update);
        }
    }
}
