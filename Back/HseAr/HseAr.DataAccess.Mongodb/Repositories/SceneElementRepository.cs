using System;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.Mongodb.Filters;
using HseAr.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HseAr.DataAccess.Mongodb.Repositories
{
    public class SceneElementRepository : ISceneElementRepository
    {
        private readonly IMongoCollection<BsonDocument> _scenes;
        private readonly IMapper<SceneModification, SceneModificationEntity> _sceneModMapper;
        private readonly IMapper<SceneModificationEntity, SceneModification> _sceneModEntityMapper;

        public SceneElementRepository(
            IMapper<SceneModification, SceneModificationEntity> sceneModMapper,
            IMapper<SceneModificationEntity, SceneModification> sceneModEntityMapper,
            MongoContext mongoContext)
        {
            _scenes = mongoContext.ScenesAsBsonDocument;
            _sceneModMapper = sceneModMapper;
            _sceneModEntityMapper = sceneModEntityMapper;
        }
        
        public async Task<UpdateResult> InsertElementToModel(SceneModification sceneMod)
        {
            var sceneModEntity = _sceneModMapper.Map(sceneMod);

            if (sceneModEntity.SceneElementType == SceneElementType.Object)
            {
                throw new Exception("Недопустимая операция для данного элемента");
            }

            if (sceneModEntity.Material == null
                || sceneModEntity.Geometry == null
                || sceneModEntity.ObjectChild == null)
            {
                throw new Exception("Для добавления объекта должны быть не нулевыми " +
                                    "поля Material, Geometry, ObjectChildren");
            }

            var update = Builders<BsonDocument>.Update
                .AddToSet("object.children", sceneModEntity.ObjectChild)
                .AddToSet("materials", sceneModEntity.Material)
                .AddToSet("geometries", sceneModEntity.Geometry);

            var filter = SceneFilter.GetFilterById(sceneModEntity.ModelId);
            
            return await _scenes.UpdateOneAsync(filter, update);
        }

        
        public async Task<UpdateResult> DeleteElementFromScene(SceneModification sceneMod)
        {
            var filter = SceneFilter.GetFilterById(sceneMod.ModelId);
            var sceneAsBson = await _scenes.Find(filter).FirstOrDefaultAsync();
            
            var uuid = sceneMod.ObjectChild["uuid"]?.ToString();
            var sectionName = GetSectionName(sceneMod.SceneElementType);
            BsonArray section;

            if (sectionName == "geometries" || sectionName == "materials")
            {
                section = sceneAsBson[sectionName].AsBsonArray;
            }
            else
            {
                section = sceneAsBson["object"][sectionName].AsBsonArray;
            }

            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            section.Remove(update);

            if (sectionName == "geometries" || sectionName == "materials")
            {
                sceneAsBson[sectionName] = section;
            }
            else
            {
                sceneAsBson["object"][sectionName] = section;
            }
            
            return await _scenes.UpdateOneAsync(filter, new BsonDocument("$set", sceneAsBson));
        }
        

        public async Task<UpdateResult> UpdateElement(SceneModification sceneMod)
        {
            var sceneModEntity = _sceneModMapper.Map(sceneMod);
            var filter = SceneFilter.GetFilterById(sceneMod.ModelId);
            var model = await _scenes.Find(filter).FirstOrDefaultAsync();
            
            var sectionName = GetSectionName(sceneModEntity.SceneElementType);

            if (sceneModEntity.PropertyModificationType == SceneModificationType.Add
                || sceneModEntity.PropertyModificationType == SceneModificationType.Update)
            {
                AddOrUpdateProperties(ref model, sceneModEntity, sectionName);
            }
            else if (sceneModEntity.PropertyModificationType == SceneModificationType.Delete)
            {
                RemoveProperty(ref model, sceneModEntity, sectionName);
            }

            var result =  _scenes.UpdateOne(filter, new BsonDocument("$set", model));
            return result;
        }
        
        
        private BsonDocument AddOrUpdateProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            switch (sectionName)
            {
                case "materials":
                    UpdateMaterialProperties(ref model, modificationEntity, sectionName);
                    break;

                case "geometries":
                    UpdateGeometryProperties(ref model, modificationEntity, sectionName);
                    break;

                case "children":
                    UpdateObjectChildProperties(ref model, modificationEntity, sectionName);
                    break;

                case "object":
                    UpdateObjectProperties(ref model, modificationEntity, sectionName);
                    break;
                default:
                    throw new Exception("Недопустимое имя");
            }

            return model;
        }

        private BsonDocument RemoveProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            switch (sectionName)
            {
                case "materials":
                    RemoveMaterialProperty(ref model, modificationEntity, sectionName);
                    break;
                case "geometries":
                    RemoveGeometryProperty(ref model, modificationEntity, sectionName);
                    break;

                case "children":
                    RemoveObjectChildProperty(ref model, modificationEntity, sectionName);
                    break;

                case "object":
                    RemoveObjectProperty(ref model, modificationEntity, sectionName);
                    break;
                default:
                    throw new Exception("Недопустимое имя");
            }

            return model;
        }

        private string GetSectionName(SceneElementType type)
        {
            switch (type)
            {
                case SceneElementType.Geometry:
                    return "geometries";

                case SceneElementType.Material:
                    return "materials";

                case SceneElementType.Object:
                    return "object";

                case SceneElementType.ObjectChild:
                    return "children";

                default:
                    throw new Exception("Invalid element");
            }
        }

        private void UpdateMaterialProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var uuid = string.Empty;
            var section = model[sectionName].AsBsonArray;
            uuid = modificationEntity.Material["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Material.Names)
            {
                section[ind][property] = modificationEntity.Material[property];
            }

            model[sectionName] = section;
        }

        private void UpdateGeometryProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var uuid = string.Empty;
            var section = model[sectionName].AsBsonArray;
            uuid = modificationEntity.Geometry["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Geometry.Names)
            {
                section[ind][property] = modificationEntity.Geometry[property];
            }

            model[sectionName] = section;
        }

        private void UpdateObjectChildProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model["object"][sectionName].AsBsonArray;
            var uuid = modificationEntity.ObjectChild["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);
            foreach (var property in modificationEntity.ObjectChild.Names)
            {
                section[ind][property] = modificationEntity.ObjectChild[property];
            }
            model["object"][sectionName] = section;
        }

        private void UpdateObjectProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            foreach (var property in modificationEntity.Object.Names)
            {
                model[sectionName][property] = modificationEntity.Object[property];
            }
        }

        private void RemoveMaterialProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model[sectionName].AsBsonArray;
            var uuid = modificationEntity.Material["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Material.Names)
            {
                section[ind].AsBsonDocument.Remove(property);
            }
            model[sectionName] = section;
        }

        private void RemoveGeometryProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model[sectionName].AsBsonArray;
            var uuid = modificationEntity.Geometry["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Geometry.Names)
            {
                section[ind].AsBsonDocument.Remove(property);
            }

            model[sectionName] = section;
        }

        private void RemoveObjectChildProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model["object"][sectionName].AsBsonArray;
            var uuid = modificationEntity.ObjectChild["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);
            foreach (var property in modificationEntity.ObjectChild.Names)
            {
                section[ind].AsBsonDocument.Remove(property);
            }
            model["object"][sectionName] = section;
        }

        private void RemoveObjectProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            foreach (var property in modificationEntity.Object.Names)
            {
                model[sectionName].AsBsonDocument.Remove(property);
            }
        }
    }
}