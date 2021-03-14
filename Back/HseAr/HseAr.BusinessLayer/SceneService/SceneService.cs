using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.SceneService.Constructors;
using HseAr.Data;
using System.Linq;
using Afisha.Tickets.Core.Linq;
using HseAr.Core.Guard;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
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

        public async Task UploadScene(Scene scene, Floor floor)
        {
            var resultStatus = await _sceneExport.ExportScene(scene);
            
            if (!resultStatus)
            {
                throw new Exception("что-то не так");
            }
                
            floor.IsLatestVersion = true;
            await _data.Floors.Update(floor);
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

        public async Task<bool> ApplyAndSaveSceneModifications(IEnumerable<SceneModification> sceneMods, Guid floorId, Guid companyId)
        {
            if (sceneMods.IsNullOrEmpty())
            {
                return true;
            }
            var sceneId = sceneMods.First().SceneId;
            
            var floor = await _data.Floors.GetById(floorId);
            Ensure.Equals(floor.SceneId, sceneId, nameof(ApplyAndSaveSceneModifications));
            
            var building = await _data.Buildings.GetById(floor.BuildingId);
            Ensure.Equals(building.CompanyId, companyId, nameof(ApplyAndSaveSceneModifications));

            
            var result = true;
            foreach (var sceneMod in sceneMods)
            {
                result &= await _data.Scenes.ApplyModification(sceneMod);
            }

            floor.IsLatestVersion = false;
            await _data.Floors.Update(floor);

            return result;
        }
        
    }
}
