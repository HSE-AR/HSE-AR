using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.SceneService;

namespace HseAr.BusinessLayer.ArClientService
{
    public class ArClientService :IArClientService
    {
        private readonly ISceneService _sceneService;
        private readonly IAccountService _accountService;
        
        public ArClientService(ISceneService sceneService, IAccountService accountService)
        {
            _sceneService = sceneService;
            _accountService = accountService;
        }
        public async Task<string> GetStartScene(Guid floorId, Guid clientKey)
        {
            var account = await _accountService.GetAccountByArClientKey(clientKey);
            var scene = await _sceneService.GetUserSceneByFloorId(floorId, account.Id);

            var sceneUrl = await _sceneService.UploadScene(scene);

            return sceneUrl;
        }

    }
}