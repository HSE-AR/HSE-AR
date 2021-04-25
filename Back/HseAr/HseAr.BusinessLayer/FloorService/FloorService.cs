using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BlenderService;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.Helpers;
using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.BusinessLayer.SceneService;
using HseAr.Core.Guard;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.DataAccess.EFCore.Repositories;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.FloorService
{
    public class FloorService : IFloorService
    {
        private const string StorageFloorplanImgs = "/floorplans/imgs/";
        private const string StorageFloorplanGltfs = "/floorplans/gltfs/";

        private readonly IBlenderService _blenderService;
        private readonly ISceneService _sceneService;
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;

        public FloorService(
            IUnitOfWork data,
            ISceneService sceneService ,
            IMapper mapper,
            IBlenderService blenderService,
            Configuration configuration)
        {
            _blenderService = blenderService;
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
            Ensure.IsNotNull(floor, nameof(_data.Floors.GetById));
                
            var currentBuilding = buildings.FirstOrDefault(b => b.Id == floor.BuildingId);
            Ensure.IsNotNull(currentBuilding, nameof(GetFloorById));

            await SetFloorIdInPointCloud(floor.PointCloudId, null);
            
            FileManager.DeleteFile($"{_configuration.STORAGE_PATH}{floor.FloorPlanImg}");
            FileManager.DeleteFile($"{_configuration.STORAGE_PATH}{floor.FloorPlanGltf}");
            
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
            
            await SetFloorIdInPointCloud(floorContext.PointCloudId, floorContext.Id);
            
            var sceneResult = await _sceneService.AddEmptyScene();
            floorContext.SceneId = sceneResult.Id;
            
            UploadFloorPlanImage(ref floorContext, floorPlanImg, floorContext.Id);

            await CreateAndSaveFloorPlanGltf(floorContext, floorContext.Id);

            var floor = _mapper.Map<FloorContext, Floor>(floorContext);
            
            var floorResult = await _data.Floors.Add(floor);
            return _mapper.Map<Floor, FloorContext>(floorResult);
        }

        public async Task UpdateFloor(FloorContext floorContext, string? img, Guid companyId)
        {
            var buildings = await _data.Buildings.GetListByCompanyId(companyId);
            Ensure.IsNotNullOrEmptySequence(buildings, nameof(_data.Buildings.GetListByCompanyId));

            var floor = await _data.Floors.GetById(floorContext.Id);
            Ensure.IsNotNull(floor, nameof(CreateFloor));
            
            var currentBuilding = buildings.FirstOrDefault(b => b.Id == floor.BuildingId);
            Ensure.IsNotNull(currentBuilding, nameof(CreateFloor));
            Ensure.Equals(currentBuilding!.Id, floorContext.BuildingId, nameof(CreateFloor));

            floor.Number = floorContext.Number;
            floor.Title = floorContext.Title;

            if (floor.PointCloudId != floorContext.PointCloudId)
            {
                await SetFloorIdInPointCloud(floor.PointCloudId, null);
                floor.PointCloudId = floorContext.PointCloudId;
                
                await SetFloorIdInPointCloud(floor.PointCloudId, floor.Id);
            }

            if (img != null)
            {
                FileManager.DeleteFile($"{_configuration.STORAGE_PATH}{floor.FloorPlanImg}");
                FileManager.DeleteFile($"{_configuration.STORAGE_PATH}{floor.FloorPlanGltf}");
                
                UploadFloorPlanImage(ref floorContext, img, floorContext.Id);
                await CreateAndSaveFloorPlanGltf(floorContext, floorContext.Id);
            }

            await _data.Floors.Update(floor);
        }

        private async Task CreateAndSaveFloorPlanGltf(FloorContext floor, Guid floorId )
        {
            if(floor.FloorPlanImg == null)
                return;
            
            var gltfPathWithoutFormat = $"{_configuration.STORAGE_PATH}{StorageFloorplanGltfs}{floorId}";
            var imagePath = $"{_configuration.STORAGE_PATH}{floor.FloorPlanImg}";

            await _blenderService.CreateFloorplanGltf(imagePath, gltfPathWithoutFormat);
            //нужно научиться проверять получилось ли создать 3д модель этажа
            
            floor.FloorPlanGltf =$"{StorageFloorplanGltfs}{floorId}.gltf";
        }
        
        private async Task SetFloorIdInPointCloud(Guid? pcdId, Guid? newValue)
        {
            if (pcdId != null)
            {
                var pointCloud = await _data.PointClouds.GetById((Guid) pcdId);
                Ensure.IsNotNull(pointCloud, nameof(_data.PointClouds.GetById));
                
                pointCloud.FloorId = newValue;
                await _data.PointClouds.Update(pointCloud);
            }
        }
        
        private void UploadFloorPlanImage(ref FloorContext floorContext, string floorPlanImage, Guid floorId)
        {
            var imagePathWithoutFormat = $"{_configuration.STORAGE_PATH}{StorageFloorplanImgs}{floorId}";
            var image = ImageManager.GetImage(floorPlanImage);
            var format = ImageManager.UploadImage(image, imagePathWithoutFormat);

            floorContext.ImgHeight = image.Height;
            floorContext.ImgWidth = image.Width;
            floorContext.FloorPlanImg = $"{StorageFloorplanImgs}{floorId}.{ImageManager.GetFormatString(format)}";
        }
    }
}