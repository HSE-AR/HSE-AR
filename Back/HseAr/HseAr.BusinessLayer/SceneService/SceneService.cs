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
using HseAr.Integration.SceneExport;

namespace HseAr.BusinessLayer.SceneService
{
    public class SceneService : ISceneService
    {
        private readonly IUnitOfWork _data;
        private readonly List<ISceneModificationHandler> _modificationHandlers;
        private readonly ISceneExportApiClient _sceneExport;

        public SceneService(IUnitOfWork data, IServiceProvider serviceProvider, ISceneExportApiClient sceneExport)
        {
            _data = data;
            _modificationHandlers = serviceProvider.GetServices<ISceneModificationHandler>().ToList();
            _sceneExport = sceneExport;
        }

        public async Task<string> UploadScene(Scene scene)
        {

            return await _sceneExport.ExportScene(scene);
        }

        public async Task<Scene> GetUserSceneByFloorId(Guid id, Guid userId)
        {
            var floor = await _data.Floors.GetById(id);

            var building = await _data.Buildings.GetById(floor.BuildingId);
            if (!building.UserBuildingEntities.Any(ub => ub.UserId == userId))
            {
                throw new Exception();
            }

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
