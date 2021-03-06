using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.FloorService;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data.DataProjections;
using HseAr.WebPlatform.Api.Attributes;
using HseAr.WebPlatform.Api.Helpers;
using HseAr.WebPlatform.Api.Models.Editor;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [AccessToCompany]
    [Route("wapi/[controller]")]
    public class EditorController : ControllerBase
    {
        private readonly ISceneService _sceneService;
        private readonly IFloorService _floorService;
        private readonly IEditorModelConstructor _editorConstructor;

        public EditorController(ISceneService sceneService, IEditorModelConstructor editorConstructor, IFloorService floorService)
        {
            _sceneService = sceneService;
            _floorService = floorService;
            _editorConstructor = editorConstructor;
        }
        
        /// <summary>
        /// получение данных для инициализации редактора
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        [HttpGet("{floorId}")]
        [Authorize]
        public async Task<ActionResult<EditorInfoModel>> InitializeEditorByFloorId(Guid floorId)
        {
            var scene = await _sceneService.GetSceneByFloorId(floorId, this.GetCompanyId());
            var floorContext = await _floorService.GetFloorById(floorId);

            return _editorConstructor.ConstructInfoModel(floorContext, scene);

        }
        
        /// <summary>
        /// Применение модификаций к сцене
        /// </summary>
        /// <remarks>
        /// Содержание поля dataJson в зависимости от типа модификации
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
        ///         "uuid": ""
        ///     }
        ///     
        /// UpdateTransform:
        /// 
        ///     {
        ///         "uuid": ""
        ///         "matrix": []
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