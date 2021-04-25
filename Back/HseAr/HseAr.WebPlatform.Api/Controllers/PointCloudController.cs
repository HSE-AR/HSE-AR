using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.PointCloudService;
using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.WebPlatform.Api.Helpers;
using HseAr.WebPlatform.Api.Models.PointCloud;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]")]
    public class PointCloudController : ControllerBase
    {
        private readonly IPointCloudService _pcdService;
        private readonly IUnitOfWork _data;
        private readonly IPointCloudModelConstructor _constructor;
        
        public PointCloudController(
            IPointCloudService pcdService,
            IPointCloudModelConstructor constructor,
            IUnitOfWork data)
        {
            _pcdService = pcdService;
            _constructor = constructor;
            _data = data;
        }

        /// <summary>
        /// Получение списка поинтклаудов (подробно)
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public async Task<ActionResult<List<PointCloudViewModel>>> GetInfo()
        {
            var pcds =  await _pcdService.GetPointClouds(this.GetCompanyId());
            var result = new List<PointCloudViewModel>();
            foreach (var pcd in pcds)
            {
                PointCloudViewModel p;
                if (pcd.FloorId != null)
                {
                    var floor = await _data.Floors.GetById((Guid) pcd.FloorId);
                    var building = await _data.Buildings.GetById(floor.BuildingId);
                    p = _constructor.ConstructModel(pcd, building, floor);
                }
                else
                {
                    p = _constructor.ConstructModel(pcd, null, null);
                }
                
                result.Add(p);
            }

            return result;
        }
        
        /// <summary>
        /// Получение списка поинтклаудов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PointCloudContext>>> Get()
        {
            return  await _pcdService.GetPointClouds(this.GetCompanyId());
        }

        
        /// <summary>
        /// Удаление поинтклауда
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PointCloudViewModel>>> Delete(Guid id)
        {
            await _pcdService.DeletePointCloud(id, this.GetCompanyId());
            
            var pcds =  await _pcdService.GetPointClouds(this.GetCompanyId());
            var result = new List<PointCloudViewModel>();
            foreach (var pcd in pcds)
            {
                PointCloudViewModel p;
                if (pcd.FloorId != null)
                {
                    var floor = await _data.Floors.GetById((Guid) pcd.FloorId);
                    var building = await _data.Buildings.GetById(floor.BuildingId);
                    p = _constructor.ConstructModel(pcd, building, floor);
                }
                else
                {
                    p = _constructor.ConstructModel(pcd, null, null);
                }
                
                result.Add(p);
            }

            return result;
        }
        
    }
}