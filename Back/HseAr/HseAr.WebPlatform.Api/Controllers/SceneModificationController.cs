using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data.DataProjections;
using HseAr.WebPlatform.Api.Attributes;
using HseAr.WebPlatform.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [AccessToCompany]
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
        /// <remarks>
        /// Содержание поля object в зависимости от типа модификации
        ///     
        /// InsertObjectToScene:
        /// 
        ///     {        
        ///         "object": {},
        ///         "material":{},
        ///         "geometry":{} 
        ///     }
        ///     
        /// DeleteObjectFromScene:
        /// 
        ///     {
        ///         "uuid": ""
        ///     }
        ///     
        /// AddLightToScene:
        /// 
        ///     {
        ///         "тело объекта света"
        ///     }
        ///     
        /// DeleteLightFromScene:
        /// 
        ///     {
        ///         "uuid":
        ///     }
        ///     
        /// </remarks>
        /// <param name="sceneModifications">список модификаций разных типов</param>
        /// <returns></returns>
        [HttpPost("list")]
        [Authorize]
        public async Task<ActionResult<bool>> SetModifications([FromBody] IEnumerable<SceneModification> sceneModifications)
        {
            return await _sceneService.ApplyAndSaveSceneModifications(sceneModifications, this.GetCompanyId());
        }
    }
}