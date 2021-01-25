using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Scene.Constructors;
using HseAr.Data.DataProjections;
using HseAr.Data.Enums;
using HseAr.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

namespace HseAr.BusinessLayer.Scene
{
    public class SceneService : ISceneService
    {
        private readonly ISceneRepository _sceneRepo;
        private readonly IFloorRepository _floorRepo; 
        private readonly ISceneModificationRepository _sceneModRepo;
        private readonly ISceneElementRepository _sceneElementRepo;

        public SceneService(
            ISceneRepository sceneRepo, 
            IFloorRepository floorRepo, 
            ISceneModificationRepository sceneModRepo,
            ISceneElementRepository sceneElementRepo)
        {
            _sceneRepo = sceneRepo;
            _floorRepo = floorRepo;
            _sceneModRepo = sceneModRepo;
            _sceneElementRepo = sceneElementRepo;
        }

        public async Task<Data.DataProjections.Scene> GetSceneByFloorId(Guid id)
        {
            var floor = await _floorRepo.GetById(id);
            return await _sceneRepo.GetById(floor.SceneId);
        }
        
        public async Task<Data.DataProjections.Scene> AddEmptyScene()
        {
            var emptyScene = EmptySceneConstructor.CreateEmptyScene();

            var sceneResult = await _sceneRepo.Create(emptyScene);
            return sceneResult;
        }
        
        public async Task<bool> ApplyAndSaveSceneModifications(IEnumerable<SceneModification> sceneMods, Guid userId)
        {
            //CheckModelOwnership(sceneModificationDto.ModelId, userId);
            var result = true;
            foreach (var sceneMod in sceneMods)
            {
                result &= await ApplySceneModification(sceneMod);
            }

            if (result)
            {
                await SaveSceneModifications(sceneMods);
            }
            
            return result;
        }
        
        private async Task<bool> ApplySceneModification(SceneModification sceneMod)
        {
            UpdateResult result;
            
            switch (sceneMod.Type)
            {
                case SceneModificationType.Add:
                    result = await _sceneElementRepo.InsertElementToModel(sceneMod);
                    break;

                case SceneModificationType.Delete:
                    result = await _sceneElementRepo.DeleteElementFromScene(sceneMod);
                    break;

                case SceneModificationType.Update:
                    result = await _sceneElementRepo.UpdateElement(sceneMod);
                    break;

                default: 
                    result = null;
                    break;
            }

            if (result == null || !result.IsAcknowledged)
            {
                return false;
            }

            return true;
        }

        private async Task SaveSceneModifications(IEnumerable<SceneModification> sceneMods)
        {
            foreach (var sceneMod in sceneMods)
            {
                await _sceneModRepo.CreateAsync(sceneMod);
            }
        }
    }
}

