using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.Helpers;
using HseAr.BusinessLayer.SceneService;
using HseAr.Core.Guard;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using MongoDB.Driver;

namespace HseAr.BusinessLayer.FloorService
{
    public class FloorService : IFloorService
    {
        private const string storageFloorplanImgs = "/floorplans/imgs/";
        
        private readonly ISceneService _sceneService;
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;

        public FloorService(
            IUnitOfWork data,
            ISceneService sceneService ,
            IMapper mapper,
            Configuration configuration)
        {
            _data = data;
            _sceneService = sceneService;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        public async Task<FloorContext> GetFloorById(Guid floorId, Guid companyId)
        {
            var buildings = await _data.Buildings.GetListByCompanyId(companyId);
            Ensure.IsNotNullOrEmptySequence(buildings, nameof(_data.Buildings.GetListByCompanyId));

            var floor = await _data.Floors.GetById(floorId);
            
            var currentBuilding = buildings.FirstOrDefault(b => b.Id == floor.BuildingId);
            Ensure.IsNotNull(currentBuilding, nameof(GetFloorById));
            
            return _mapper.Map<Floor, FloorContext>(floor);
        }

        public async Task DeleteFloor(Guid floorId, Guid companyId)
        {
            var buildings = await _data.Buildings.GetListByCompanyId(companyId);
            Ensure.IsNotNullOrEmptySequence(buildings, nameof(_data.Buildings.GetListByCompanyId));

            var floor = await _data.Floors.GetById(floorId);
                
            var currentBuilding = buildings.FirstOrDefault(b => b.Id == floor.BuildingId);
            Ensure.IsNotNull(currentBuilding, nameof(GetFloorById));

            ImageManager.DeleteImage($"{_configuration.STORAGE_PATH}{floor.FloorPlanImg}");
            await _data.Scenes.Remove(floor.SceneId);
            await _data.Floors.Delete(floor.Id);
        }

        public async Task<FloorContext> CreateFloor(FloorContext floorContext, string floorPlanImg, Guid companyId)
        {
            var buildings = await _data.Buildings.GetListByCompanyId(companyId);
            Ensure.IsNotNullOrEmptySequence(buildings, nameof(_data.Buildings.GetListByCompanyId));

            var currentBuilding = buildings.FirstOrDefault(b => b.Id == floorContext.BuildingId);
            Ensure.IsNotNull(currentBuilding, nameof(CreateFloor));
            
            floorContext.Id = Guid.NewGuid();
            
            var sceneResult = await _sceneService.AddEmptyScene();
            floorContext.SceneId = sceneResult.Id;
            
            UploadFloorPlanImage(ref floorContext, floorPlanImg, floorContext.Id);

            var floor = _mapper.Map<FloorContext, Floor>(floorContext);
            
            var floorResult = await _data.Floors.Add(floor);
            return _mapper.Map<Floor, FloorContext>(floorResult);
        }

        private void UploadFloorPlanImage(ref FloorContext floorContext, string floorPlanImage, Guid floorId)
        {
            var imagePathWithoutFormat = $"{_configuration.STORAGE_PATH}{storageFloorplanImgs}{floorId}";
            var image = ImageManager.GetImage(floorPlanImage);
            var format = ImageManager.UploadImage(image, imagePathWithoutFormat);

            floorContext.ImgHeight = image.Height;
            floorContext.ImgWidth = image.Width;
            floorContext.FloorPlanImg = $"{storageFloorplanImgs}{floorId}.{ImageManager.GetFormatString(format)}";
        }
    }
}