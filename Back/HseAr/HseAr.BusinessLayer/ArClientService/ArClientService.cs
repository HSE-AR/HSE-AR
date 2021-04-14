using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.SceneService;
using HseAr.Core.Guard;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.ArClientService
{
    public class ArClientService :IArClientService
    {
        private const int MaxDistance = 10;
        
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

        public async Task<List<Building>> GetArPlaces(Guid clientKey, double? lat, double? lon )
        {
            var arClient = await _data.ArClients.GetById(clientKey);
            var companies = arClient.Companies;
            
            var resultBuildings = new List<Building>();
            
            foreach (var company in companies)
            {
                var buildings = await _data.Buildings.GetListByCompanyId(company.Id);
                foreach (var building in buildings)
                {
                    if (lat != null && lon != null)
                    {
                        var distance = GetDistance(building.Longitude, building.Latitude, (double)lon, (double)lat);
                        if (distance > MaxDistance)
                        {
                            continue;
                        }
                    }
                    
                    resultBuildings.Add(building);
                }
            }

            return resultBuildings;
        }

        public async Task<Building> GetArPlaceById(Guid clientKey, Guid id)
        {
            var building = await _data.Buildings.GetById(id);
            Ensure.IsNotNull(building, nameof(_data.Buildings.GetById));
            
            var company = await _data.Companies.GetById(building.CompanyId);
            Ensure.Equals(company.ArClientId, clientKey, "ошибка доступа");

            return building;
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - x2, 2));
        }
    }
}