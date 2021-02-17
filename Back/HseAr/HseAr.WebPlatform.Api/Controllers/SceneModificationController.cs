using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data.Entities;
using HseAr.WebPlatform.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/test")]
    public class SceneModificationController : ControllerBase
    {
        private readonly ISceneService _sceneService;

        public SceneModificationController(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }
        
        /// <summary>
        /// Применение модификаций к сцене
        /// </summary>
        /// <param name="sceneModifications">список модификаций разных типов</param>
        /// <returns></returns>
        [HttpPost("list")]
        [Authorize]
        public async Task<ActionResult<bool>> SetModifications([FromBody] IEnumerable<SceneModification> sceneModifications)
        {
            var userId = this.GetUserIdFromToken();
            return await _sceneService.ApplyAndSaveSceneModifications(sceneModifications, userId);
        }
    }
}