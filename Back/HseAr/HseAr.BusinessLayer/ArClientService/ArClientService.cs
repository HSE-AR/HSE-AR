using System;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data;
using HseAr.Data.Entities;

namespace HseAr.BusinessLayer.ArClientService
{
    public class ArClientService :IArClientService
    {
        private readonly ISceneService _sceneService;
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _data;
        
        public ArClientService(
            ISceneService sceneService,
            IAccountService accountService,
            IUnitOfWork data)
        {
            _sceneService = sceneService;
            _accountService = accountService;
            _data = data;
        }
        
        public async Task<string> GetStartScene( Guid floorId, Guid clientKey)
        {
            var floor = await _data.Floors.GetById(floorId);
            var building = await _data.Buildings.GetById(floor.BuildingId);
            var arClient = await _data.ArClients.GetById(clientKey);

            if (!arClient.Companies.Any(company => company.Id == building.CompanyId))
            {
                throw new Exception("Ошибка доступа");
            }

            var scene = await _data.Scenes.GetById(floor.SceneId);

            return await _sceneService.UploadScene(scene);
        }

    }
}