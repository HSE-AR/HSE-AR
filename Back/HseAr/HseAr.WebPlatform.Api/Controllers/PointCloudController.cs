using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.PointCloudService;
using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Data.Entities;
using HseAr.WebPlatform.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]")]
    public class PointCloudController : ControllerBase
    {
        private readonly IPointCloudService _pcdService;
        
        public PointCloudController(IPointCloudService pcdService)
        {
            _pcdService = pcdService;
        }

        /// <summary>
        /// Получение списка поинтклаудов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PointCloudContext>>> Get()
        {
            return await _pcdService.GetPointClouds(this.GetCompanyId());
        }
        
        /// <summary>
        /// Получение списка поинтклаудов
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PointCloudContext>>> Delete(Guid id)
        {
            await _pcdService.DeletePointCloud(id, this.GetCompanyId());
            return await _pcdService.GetPointClouds(this.GetCompanyId());
        }
        
    }
}