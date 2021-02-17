using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.SceneService.Constructors;
using HseAr.Data;
using HseAr.Data.DataProjections;
using System.Linq;
using HseAr.Integration.SceneExport;

namespace HseAr.BusinessLayer.SceneService
{
    public class SceneService : ISceneService
    {
        private readonly IUnitOfWork _data;
        private readonly ISceneExportApiClient _sceneExport;

        public SceneService(IUnitOfWork data, ISceneExportApiClient sceneExport)
        {
            _data = data;
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
                result &= await _data.Scenes.ApplyModification(sceneMod);
            }

            if (result)
            {
                await SaveSceneModifications(sceneMods);
            }

            return result;
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
