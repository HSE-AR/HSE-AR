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
        /// Получение зданий ArКлиента
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ArPlacesViewModel>> Get()
        {
            var buildings = await _arClientService.GetArPlaces(this.GetArClientKey());
            return _constructor.ConstructArPlace(buildings);
        }
        
    }
}