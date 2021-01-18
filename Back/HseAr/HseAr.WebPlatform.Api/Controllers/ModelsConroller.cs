using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Model;
using HseAr.Data.DTO;
using HseAr.Data.Entities;
using HseAr.DataAccess.Mongodb.Repositories;
using HseAr.WebPlatform.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
        [Route("wapi/[controller]")]
        public class ModelsController : BaseAuthorizeController
        {
            private readonly ModelsRepository _modelsRepository;
            private readonly IModelService _modelsService;

            public ModelsController(ModelsRepository modelsRepository, IModelService modelsService)
            {
                _modelsRepository = modelsRepository;
                _modelsService = modelsService;
            }

            [HttpGet]
            [Authorize(Roles = "superadmin")]
            public async Task<ActionResult<ICollection<ModelDto>>> Get() =>
                 new JsonResult(await _modelsRepository.GetAsync());
 

            [HttpGet("{id:length(24)}", Name = "GetModel")]
            [Authorize(Roles = "superadmin")]
            public async Task<ActionResult<ModelDto>> Get(string id)
            {
                var model = await _modelsRepository.GetAsync(id);

                if (model == null)
                {
                    return NotFound();
                }

                return model;
            }

            [HttpGet("user")]
            [Authorize]
            public async Task<ActionResult<ICollection<ModelDto>>> GetUserModels()
            {
                try
                {
                    var userId = GetUserIdFromToken();
                    
                    return new JsonResult(await _modelsService.GetUserModelsAsync(userId));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            [HttpPost]
            [Authorize]
            public async Task<ActionResult<ModelDto>> Create([FromBody]ModelDto modelDto)
            {
                var userId = GetUserIdFromToken();
                await _modelsService.CreateModelAsync(modelDto, userId);
                return Ok();
                    
            }

            [HttpPut("{id:length(24)}")]
            [Authorize]
            public async Task<IActionResult> Update(string id, Model modelIn)
            {
                var model = _modelsRepository.GetAsync(id);

                if (model == null)
                {
                    return NotFound();
                }

                await _modelsRepository.UpdateAsync(id, modelIn);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            [Authorize]
            public async Task<IActionResult> Delete(string id)
            {
                try
                {
                    var userId = GetUserIdFromToken();
                    await _modelsService.DeleteModelAsync(id, userId);
                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex);
                } 
            }
    }
}