using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.SceneService.Constructors;
using HseAr.Data;
using System.Linq;
using HseAr.Data.DataProjections;
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

        public async Task<Scene> GetSceneByFloorId(Guid floorId, Guid companyId)
        {
            var floor = await _data.Floors.GetById(floorId);

            var company = await _data.Companies.GetById(companyId);
            if (!company.Buildings.Any(b => b.Id == floor.BuildingId))
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

        public async Task<bool> ApplyAndSaveSceneModifications(IEnumerable<SceneModification> sceneMods, Guid companyId)
        {
            //CheckModelOwnership(sceneModificationDto.ModelId, userId);
            var result = true;
            foreach (var sceneMod in sceneMods)
            {
                result &= await _data.Scenes.ApplyModification(sceneMod);
            }
            
            return result;
        }
        
    }
}
