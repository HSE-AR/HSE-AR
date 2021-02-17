using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.Mongodb.Filters;
using HseAr.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace HseAr.DataAccess.Mongodb.SceneModificationHandlers
{
    public class InsertElementToModelHandler : ISceneModificationHandler
    {
        private readonly IMongoCollection<BsonDocument> _scenes;
        private readonly IMapper<SceneModification, SceneModificationEntity> _sceneModMapper;

        public InsertElementToModelHandler(
            IMapper<SceneModification, SceneModificationEntity> sceneModMapper,
            MongoContext mongoContext)
        {
            _scenes = mongoContext.ScenesAsBsonDocument;
            _sceneModMapper = sceneModMapper;
        }

        public InsertElementToModelHandler()
        {
        }

        public async Task<UpdateResult> Modify(SceneModification sceneMod)
        {
            var sceneModEntity = _sceneModMapper.Map(sceneMod);


            if (sceneModEntity.Object["material"] == null
                || sceneModEntity.Object["geometry"] == null
                || sceneModEntity.Object["object"] == null) //object или всё же objectChild?
            {
                throw new Exception("Для добавления объекта должны быть не нулевыми " +
                                    "поля Material, Geometry, ObjectChildren");
            }

            var update = Builders<BsonDocument>.Update
                .AddToSet("object.children", sceneModEntity.Object["object"])
                .AddToSet("materials", sceneModEntity.Object["material"])
                .AddToSet("geometries", sceneModEntity.Object["geometry"]);

            var filter = SceneFilter.GetFilterById(sceneModEntity.ModelId);

            return await _scenes.UpdateOneAsync(filter, update);
        }

        public bool CatchTypeMatch(string modificationName)
        {
            if (modificationName == "InsertElementToModel")
                return true;

            return false;
        }
    }
}
