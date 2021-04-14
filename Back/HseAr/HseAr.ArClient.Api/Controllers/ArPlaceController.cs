using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.ArClient.Api.Helpers;
using HseAr.ArClient.Api.Models;
using HseAr.ArClient.Api.ViewModelConstructor;
using HseAr.BusinessLayer.ArClientService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.ArClient.Api.Controllers
{
    [Route("arapi/[controller]")]
    public class ArPlaceController : ControllerBase
    {
        private readonly IArClientService _arClientService;
        private readonly IArPlacesModelConstructor _constructor;
        
        public ArPlaceController(IArClientService arClientService, IArPlacesModelConstructor constructor)
        {
            _arClientService = arClientService;
            _constructor = constructor;
        }

        /// <summary>
        /// Получение зданий ArКлиента поблизости 
        /// </summary>
        /// <returns></returns>
        [HttpGet("lat/{lat}/lon/{lon}")]
        public async Task<ActionResult<ArPlacesViewModel>> GetNearby(double lat, double lon)
        {
            var buildings = await _arClientService.GetArPlaces(this.GetArClientKey(), lat, lon);
            return _constructor.ConstructArPlaces(buildings);
        }
        
        /// <summary>
        /// Получение всех зданий ArКлиента
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ArPlacesViewModel>> Get()
        {
            var buildings = await _arClientService.GetArPlaces(this.GetArClientKey(), lat:null, lon:null);
            return _constructor.ConstructArPlaces(buildings);
        }

        /// <summary>
        /// Получение информации о конкретном здании
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ArPlaceInfoViewModel>> Get(Guid id)
        {
            var building = await _arClientService.GetArPlaceById(this.GetArClientKey(), id);
            return _constructor.ConstructArPlaceInfo(building);
        }
    }
}