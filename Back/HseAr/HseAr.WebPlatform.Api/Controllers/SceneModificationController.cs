using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Modification;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Mappers;
using HseAr.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/test")]
    public class SceneModificationController : BaseAuthorizeController
    {
        private readonly IModificationService _modificationService;
        private readonly IMapper _mapper;

        public SceneModificationController(IModificationService modificationService, IMapper mapper)
        {
            _mapper = mapper;
            _modificationService = modificationService;
        }
        
        [HttpGet]
        public ActionResult<string> Test()
        {
            var result = _mapper.Map<TestSource, TestResult>(new TestSource()
            {
                FieldSource = "qwerty"
            });
            return result.FieldResult ;
        }
        
        
        /// <summary>
        /// Применение модификаций к сцене
        /// </summary>
        /// <param name="modificationDtos">список модификаций разных типов</param>
        /// <returns></returns>
        [HttpPost("list")]
        [Authorize]
        public async Task<ActionResult<bool>> SetModifications([FromBody] IEnumerable<SceneModification> modificationDtos)
        {
            var userId = GetUserIdFromToken();
            return await _modificationService.ModifyModels(modificationDtos, userId);
        }
    }
}