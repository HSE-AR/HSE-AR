using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Scene;
using HseAr.Data.DataProjections;
using HseAr.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/test")]
    public class SceneModificationController : BaseAuthorizeController
    {
        private readonly ISceneService _sceneService;
        private readonly IMapper _mapper;

        public SceneModificationController(ISceneService sceneService, IMapper mapper)
        {
            _mapper = mapper;
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
            var userId = GetUserIdFromToken();
            return await _sceneService.ApplyAndSaveSceneModifications(sceneModifications, userId);
        }
    }
}