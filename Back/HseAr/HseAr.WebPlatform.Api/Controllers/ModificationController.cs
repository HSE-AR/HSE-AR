﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Modification;
using HseAr.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    //в чистовом проекте архитектура будет другая
    //шаблонный репозиторий для запросов к бд для всех классов
    //остальная логика реализована в сервисах
    //в контроллерах только обращение к сервисам
    //убрать конвертеры, реализовать данную логику через контсрукторы и др.
    [Route("wapi/[controller]")]
    public class ModificationController : Controller
    {
        private readonly IModificationService _modificationService;

        public ModificationController(IModificationService modificationService)
        {
            _modificationService = modificationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModificationDto>>> GetAsync()
        {
            try
            {
                return new JsonResult(await _modificationService.GetAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SetModification([FromBody] ModificationDto modificationDto)
        {
            try
            {
                return await _modificationService.ModifyModel(modificationDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list")]
        public async Task<ActionResult<bool>> SetModifications([FromBody] IEnumerable<ModificationDto> modificationDtos)
        {
            try
            {
                return await _modificationService.ModifyModels(modificationDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}