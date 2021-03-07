using HseAr.Data;
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
        
        public bool CatchTypeMatch(SceneModificationType modificationType)
        {
            return modificationType == SceneModificationType.DeleteObjectFromScene;
        }

        public async Task<UpdateResult> Modify(SceneModification sceneMod)
        {
            var filter = SceneFilter.GetFilterById(sceneMod.SceneId);
            var sceneAsBson = await _scenes.Find(filter).FirstOrDefaultAsync();

            var uuid = sceneMod.DataJson["uuid"]?.ToString();

            var objects = sceneAsBson["object"]["children"].AsBsonArray;
            var objectToDelete = objects.FirstOrDefault(x => x["uuid"] == uuid);

            FindAndDeleteElementsRecursively(objectToDelete.AsBsonDocument, sceneAsBson);
                
            objects.Remove(objectToDelete);
            sceneAsBson["object"]["children"] = objects;

            return await _scenes.UpdateOneAsync(filter, new BsonDocument("$set", sceneAsBson));
        }
        
        private void FindAndDeleteElementsRecursively(BsonDocument objectToDelete, BsonDocument scene)
        {
            if(objectToDelete.Contains("geometry"))
            {
                DeleteGeometry(ref scene, objectToDelete["geometry"].ToString());
            }
            
            if(objectToDelete.Contains("animations"))
            {
                DeleteAnimations(ref scene, objectToDelete["animations"].AsBsonArray);
            }
            
            if (objectToDelete.Contains("material"))
            {
                DeleteMaterialAndTexture(ref scene, objectToDelete["material"].ToString());
            }
            
            if(objectToDelete.Contains("children"))
            {
                foreach (var obj in  objectToDelete["children"].AsBsonArray)
                {
                    FindAndDeleteElementsRecursively(obj.AsBsonDocument, scene);
                }
            }
        }

        private void DeleteGeometry(ref BsonDocument scene, string geometryUuid)
        {
            var geometries = scene["geometries"].AsBsonArray;
            var geometryToDelete = geometries.FirstOrDefault(x => x["uuid"] == geometryUuid);
            if (geometryToDelete == null)
            {
                return;
            }
            geometries.Remove(geometryToDelete);
            scene["geometries"] = geometries;
        }

        private void DeleteAnimations(ref BsonDocument scene, BsonArray animsToDelete)
        {
            var animations = scene["animations"].AsBsonArray;
            foreach (var anim in animsToDelete)
            {
                var animationToDelete = animations.FirstOrDefault(x => x["uuid"] == anim.ToString());
                if (animationToDelete == null)
                {
                    continue;
                }
                
                animations.Remove(animationToDelete);
            }
            scene["animations"] = animations;
        }
        
        private void DeleteMaterialAndTexture(ref BsonDocument scene, string materialUuid)
        {
            var materials = scene["materials"].AsBsonArray;
            var materialToDelete = materials.FirstOrDefault(x => x["uuid"] == materialUuid);

            if (materialToDelete == null)
            {
                return;
            }

            if(materialToDelete.AsBsonDocument.Contains("map"))
            {
                DeleteTexture(ref scene, materialToDelete["map"].ToString());
            }
            
            materials.Remove(materialToDelete);
            scene["materials"] = materials;
        }
        
        private void DeleteTexture(ref BsonDocument scene, string textureUuid)
        {
            var textures = scene["textures"].AsBsonArray;
            var images = scene["images"].AsBsonArray;
            
            var textureToDelete = textures.FirstOrDefault(x => x["uuid"] == textureUuid);
            if (textureToDelete == null)
            {
                return;
            }
            var imageToDelete =  images.FirstOrDefault(x => x["uuid"] == textureToDelete["image"].ToString());
            
            textures.Remove(textureToDelete);
            images.Remove(imageToDelete);
            
            scene["textures"] = textures;
            scene["images"] = images;
        }
    }
}
