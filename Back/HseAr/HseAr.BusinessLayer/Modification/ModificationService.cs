using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
using HseAr.Data.Interfaces;
using HseAr.DataAccess.Mongodb;
using HseAr.DataAccess.Mongodb.Repositories;
using HseAr.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HseAr.BusinessLayer.Modification
{
    public class ModificationService : IModificationService
    {
        private readonly SceneModificationRepository _sceneModificationRepository;
        //нужен прямой доступ к коллекции (а не к репозиторию), для экономии памяти и времени 
        private readonly IMongoCollection<BsonDocument> _models;
        private readonly IMapper<SceneModificationEntity, SceneModification> _mapperToDto;
        private readonly IMapper<SceneModification, SceneModificationEntity> _mapperToEntity;


        public ModificationService(
            MongoContext context,
            SceneModificationRepository modRep,
            IMapper<SceneModificationEntity,SceneModification> mapperToDto,
            IMapper<SceneModification,SceneModificationEntity> mapperToEntity
            )
        {
            _sceneModificationRepository = modRep;
            _models = context.ModelsAsBsonDocument;
            _mapperToDto = mapperToDto;
            _mapperToEntity = mapperToEntity;
        }

        public async Task<IEnumerable<SceneModification>> GetAsync() =>
            (await _sceneModificationRepository.GetAsync()).Select(x => _mapperToDto.Map(x));

        public async Task<bool> ModifyModel(SceneModification sceneModificationDto, Guid userId)
        {
            //CheckModelOwnership(sceneModificationDto.ModelId, userId);
            var mod = _mapperToEntity.Map(sceneModificationDto);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(mod.ModelId));
            UpdateResult result;

            switch (mod.Type)
            {
                case SceneModificationType.Add:
                    result = await AddElementsToModelAsync(filter, mod);
                    break;

                case SceneModificationType.Delete:
                    result = await DeleteElementsFromModel(filter, mod);
                    break;

                case SceneModificationType.Update:
                    result = await UpdateModelElementsAsync(filter, mod);
                    break;

                default: 
                    result = null;
                    break;
            }

            if (result == null || !result.IsAcknowledged)
                return false;

            await _sceneModificationRepository.CreateAsync(mod);
            return true;
        }

        public async Task<bool> ModifyModels(IEnumerable<SceneModification> modificationDtos, Guid userId)
        {
            bool result = true;
            foreach (var moddto in modificationDtos)
                result &= await ModifyModel(moddto, userId);

            return result;
        }

        //надо подправить
        /*private bool CheckModelOwnership(string modelId, Guid userId)
        {
            var userModelId = _userModelIdRepository.GetAsync(modelId, userId);
            if (userModelId == null)
                throw new Exception("User has not rights to edit this model");
            return true;
        }*/

        private async Task<UpdateResult> AddElementsToModelAsync(FilterDefinition<BsonDocument> filter, SceneModificationEntity modificationEntity)
        {
            if  (modificationEntity.SceneComponentType == SceneComponentType.Object)
            //сделать кастомное исключение, чтобы потом отлавливать в Middleware
                throw new Exception("Недопустимая операция для данного элемента");



            if (modificationEntity.Material == null || modificationEntity.Geometry == null ||
                                                        modificationEntity.ObjectChild == null)

                throw new Exception("Для добавления объекта должны быть не нулевыми " +
                                                       "поля Material, Geometry, ObjectChildren");

            var update = Builders<BsonDocument>.Update
                .AddToSet("Scene.object.children", modificationEntity.ObjectChild)
                .AddToSet("Scene.materials", modificationEntity.Material)
                .AddToSet("Scene.geometries", modificationEntity.Geometry);

            return await _models.UpdateOneAsync(filter, update);
        }

        private async Task<UpdateResult> UpdateModelElementsAsync(FilterDefinition<BsonDocument> filter, SceneModificationEntity modificationEntity)
        {
            var model = await _models.Find(filter).FirstOrDefaultAsync();
            
            var sectionName = GetSectionName(modificationEntity.SceneComponentType);
           

            if (modificationEntity.PropertyModificationType == SceneModificationType.Add ||
                modificationEntity.PropertyModificationType == SceneModificationType.Update)
                AddOrUpdateProperties(ref model, modificationEntity, sectionName);

            else if (modificationEntity.PropertyModificationType == SceneModificationType.Delete) 
                RemoveProperty(ref model, modificationEntity, sectionName);
           

            var result =  _models.UpdateOne(filter, new BsonDocument("$set", model));
            return result;
        }

        private async Task<UpdateResult> DeleteElementsFromModel(FilterDefinition<BsonDocument> filter, SceneModificationEntity modificationEntity)
        {
            
            var model = await _models.Find(filter).FirstOrDefaultAsync();
            var uuid = modificationEntity.Object["uuid"].ToString();
            var sectionName = GetSectionName(modificationEntity.SceneComponentType);

            BsonArray section;

            if (sectionName == "geometries" || sectionName == "materials")
                section = model["Scene"][sectionName].AsBsonArray;
            else
                section = model["Scene"]["object"][sectionName].AsBsonArray;

            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            section.Remove(update);

            if (sectionName == "geometries" || sectionName == "materials")
                model["Scene"][sectionName] = section;
            else
                model["Scene"]["object"][sectionName] = section;

            return await _models.UpdateOneAsync(filter, new BsonDocument("$set", model));

        } 

        private BsonDocument AddOrUpdateProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            string uuid = string.Empty;
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
            string uuid = string.Empty;
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

        private string GetSectionName(SceneComponentType type)
        {
            switch (type)
            {
                case SceneComponentType.Geometry:
                    return "geometries";

                case SceneComponentType.Material:
                    return "materials";

                case SceneComponentType.Object:
                    return "object";

                case SceneComponentType.ObjectChild:
                    return "children";

                default:
                    throw new Exception("Invalid element");
            }
        }

        private void UpdateMaterialProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            string uuid = string.Empty;
            var section = model["Scene"][sectionName].AsBsonArray;
            uuid = modificationEntity.Material["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Material.Names)
            {
                section[ind][property] = modificationEntity.Material[property];
            }

            model["Scene"][sectionName] = section;
        }

        private void UpdateGeometryProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            string uuid = string.Empty;
            var section = model["Scene"][sectionName].AsBsonArray;
            uuid = modificationEntity.Geometry["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Geometry.Names)
            {
                section[ind][property] = modificationEntity.Geometry[property];
            }

            model["Scene"][sectionName] = section;
        }

        private void UpdateObjectChildProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model["Scene"]["object"][sectionName].AsBsonArray;
            var uuid = modificationEntity.ObjectChild["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);
            foreach (var property in modificationEntity.ObjectChild.Names)
            {
                section[ind][property] = modificationEntity.ObjectChild[property];
            }
            model["Scene"]["object"][sectionName] = section;
        }

        private void UpdateObjectProperties(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            foreach (var property in modificationEntity.Object.Names)
            {
                model["Scene"][sectionName][property] = modificationEntity.Object[property];
            }
        }

        private void RemoveMaterialProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model["Scene"][sectionName].AsBsonArray;
            var uuid = modificationEntity.Material["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Material.Names)
            {
                section[ind].AsBsonDocument.Remove(property);
            }
            model["Scene"][sectionName] = section;
        }

        private void RemoveGeometryProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model["Scene"][sectionName].AsBsonArray;
            var uuid = modificationEntity.Geometry["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);

            foreach (var property in modificationEntity.Geometry.Names)
            {
                section[ind].AsBsonDocument.Remove(property);
            }

            model["Scene"][sectionName] = section;
        }

        private void RemoveObjectChildProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            var section = model["Scene"]["object"][sectionName].AsBsonArray;
            var uuid = modificationEntity.ObjectChild["uuid"].ToString();
            var update = section.FirstOrDefault(x => x["uuid"] == uuid);
            var ind = section.IndexOf(update);
            foreach (var property in modificationEntity.ObjectChild.Names)
            {
                section[ind].AsBsonDocument.Remove(property);
            }
            model["Scene"]["object"][sectionName] = section;
        }

        private void RemoveObjectProperty(ref BsonDocument model, SceneModificationEntity modificationEntity, string sectionName)
        {
            foreach (var property in modificationEntity.Object.Names)
            {
                model["Scene"][sectionName].AsBsonDocument.Remove(property);
            }
        }

    }    
}