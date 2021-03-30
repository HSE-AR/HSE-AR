using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.ArClientService
{
    public class ArClientService :IArClientService
    {
        private readonly ISceneService _sceneService;
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        
        public ArClientService(
            ISceneService sceneService,
            IAccountService accountService,
            IUnitOfWork data,
            IMapper mapper)
        {
            _sceneService = sceneService;
            _accountService = accountService;
            _data = data;
            _mapper = mapper;
        }
        
        public async Task<string> GetStartScene(Guid floorId, Guid clientKey)
        {
            var floor = await _data.Floors.GetById(floorId);
            var building = await _data.Buildings.GetById(floor.BuildingId);
            var arClient = await _data.ArClients.GetById(clientKey);

            if (!arClient.Companies.Any(company => company.Id == building.CompanyId))
            {
                throw new Exception("этаж не содержится у данного ar клиента");
            }
            
            if (!floor.IsLatestVersion)
            {
                var scene = await _data.Scenes.GetById(floor.SceneId);
                await _sceneService.UploadScene(scene, floor);
            }
            
            return floor.GltfScene;
        }

        public async Task<List<Building>> GetArPlaces(Guid clientKey)
        {
            var arClient = await _data.ArClients.GetById(clientKey);
            var companies = arClient.Companies;
            
            var resultBuildings = new List<Building>();
            
            foreach (var company in companies)
            {
                var buildings = await _data.Buildings.GetListWithFloorsByCompanyId(company.Id);
                foreach (var building in buildings)
                {
                    resultBuildings.Add(building);
                }
            }

            return resultBuildings;
        }
    }
}