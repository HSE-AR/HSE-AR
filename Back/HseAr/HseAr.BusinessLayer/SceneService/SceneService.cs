﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.SceneService.Constructors;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Data.Enums;
using HseAr.Data.Interfaces;
using MongoDB.Driver;
using HseAr.DataAccess.Mongodb.SceneModificationHandlers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace HseAr.BusinessLayer.SceneService
{
    public class SceneService : ISceneService
    {
        private readonly IUnitOfWork _data;
        private readonly List<ISceneModificationHandler> _modificationHandlers;

        public SceneService(IUnitOfWork data, IServiceProvider serviceProvider)
        {
            _data = data;
            _modificationHandlers = serviceProvider.GetServices<ISceneModificationHandler>().ToList();
        }

        public async Task<Scene> GetSceneByFloorId(Guid id)
        {
            var floor = await _data.Floors.GetById(id);
            return await _data.Scenes.GetById(floor.SceneId);
        }
        
        public async Task<Scene> AddEmptyScene()
        {
            var emptyScene = EmptySceneConstructor.CreateEmptyScene();

            var sceneResult = await _data.Scenes.Create(emptyScene);
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
            UpdateResult result = null;


            foreach (ISceneModificationHandler handler in _modificationHandlers)
            {
                if (handler.CatchTypeMatch(sceneMod.Type.ToString()))
                {
                    result = await handler.Modify(sceneMod);
                    break;
                }
            }


            //switch (sceneMod.Type)
            //{
            //    case SceneModificationType.Add:
            //        result = await _data.SceneElements.InsertElementToModel(sceneMod);
            //        break;

            //    case SceneModificationType.Delete:
            //        result = await _data.SceneElements.DeleteElementFromScene(sceneMod);
            //        break;

            //    case SceneModificationType.Update:
            //        result = await _data.SceneElements.UpdateElement(sceneMod);
            //        break;

            //    default: 
            //        result = null;
            //        break;
            //}

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
                await _data.SceneModifications.CreateAsync(sceneMod);
            }
        }
    }
}

