using HseAr.BusinessLayer.PointCloudService;
using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Data;
using HseAr.Infrastructure;
using HseAr.Scanner.Api.Attributes;
using HseAr.Scanner.Api.Helpers;
using HseAr.Scanner.Api.Models.PointCloud;
using HseAr.Scanner.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HseAr.Scanner.Api.Controllers
{
    [Authorize]
    [AccessToCompany]
    [Route("sapi/[controller]")]
    public class PointCloudController : ControllerBase
    {
        private readonly IPointCloudService _pointCloudService;
        private readonly IPointCloudModelConstructor _pointCloudModelConstructor;
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;

        public PointCloudController(IPointCloudService pointCloudService, IUnitOfWork data, IMapper mapper,
            IPointCloudModelConstructor pointCloudModelConstructor)
        {
            _pointCloudService = pointCloudService;
            _data = data;
            _mapper = mapper;
            _pointCloudModelConstructor = pointCloudModelConstructor;
        }

        /// <summary>
        /// Добавление облака точек компании
        /// </summary>
        /// <param name="form"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PointCloudCurrentViewModel>> AddPointCloud(
            PointCloudCreationForm form, IFormFile file)
        {
            var cloudContext = _mapper.Map<PointCloudCreationForm, PointCloudContext>(form);
            var result = await _pointCloudService.AddPointCloudToCompany(cloudContext, file, this.GetCompanyId());
            return _pointCloudModelConstructor.ConstructCurrentModel(result);
        }
        
        /// <summary>
        /// Получения списка доступных поинтклаудов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PointCloudContext>>> Get()
        {
            return await _pointCloudService.GetPointClouds(this.GetCompanyId());
        }
    }
}
