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
using System.Linq;

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
            var filter = SceneFilter.GetFilterById(sceneMod.SceneId);
            var sceneAsBson = await _scenes.Find(filter).FirstOrDefaultAsync();

            BsonValue elements;
            
            if (sceneModEntity.DataBson.TryGetValue("materials", out elements))
            {
                sceneAsBson["materials"].AsBsonArray.AddRange(elements.AsBsonArray);
            }

            if (sceneModEntity.DataBson.TryGetValue("geometries", out elements))
            {
                sceneAsBson["geometries"].AsBsonArray.AddRange(elements.AsBsonArray);
            }
            
            if (sceneModEntity.DataBson.TryGetValue("textures", out elements))
            {
                sceneAsBson["textures"].AsBsonArray.AddRange(elements.AsBsonArray);
            }
            
            if (sceneModEntity.DataBson.TryGetValue("images", out elements))
            {
                sceneAsBson["images"].AsBsonArray.AddRange(elements.AsBsonArray);
            }
            
            if (sceneModEntity.DataBson.TryGetValue("animations", out elements))
            {
                sceneAsBson["animations"].AsBsonArray.AddRange(elements.AsBsonArray);
            }
            
            var sceneObject = sceneAsBson["object"].AsBsonDocument;

            InsertObject(ref sceneObject, ref sceneModEntity);
            
            return await _scenes.UpdateOneAsync(filter, new BsonDocument("$set", sceneAsBson));
        }

        private void InsertObject(ref BsonDocument currentObject,
           ref SceneModificationBson sceneModEntity)
        {
            var obj = sceneModEntity.DataBson["object"];

            if (obj != null)
            {
                if (!currentObject.AsBsonDocument.Contains("children"))
                {
                    currentObject.InsertAt(0, new BsonElement("children", new BsonArray()));
                }
                currentObject.Set("children", new BsonArray(currentObject["children"].AsBsonArray.Append(obj)));
            }
        }
    }
}
