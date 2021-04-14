using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.FloorService;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.Models.Floor;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public class BuildingModelConstructor : IBuildingModelConstructor
    {
        private readonly IUnitOfWork _data;
        
        public BuildingModelConstructor(IUnitOfWork data)
        {
            _data = data;
        }

        public async Task<BuildingCurrentViewModel> ConstructCurrentModel(BuildingContext source)
        {
            var floors = new List<FloorItemModel>();
            foreach (var floorId in source.FloorIds)
            {
                floors.Add(await ConstructFloorItemModel(floorId));
            }
        
            return new BuildingCurrentViewModel() 
            {
                BuildingInfo = new BuildingInfoModel()
                {
                    Id = source.Id,
                    Title = source.Title,
                    Address  = source.Address,
                    Latitude = source.Latitude,
                    Longitude = source.Longitude,
                    Floors = floors
                }
            };
        }


        public BuildingsViewModel ConstructModels(List<BuildingContext> source)
        {
            var buildings =  source.Select(building =>
                new BuildingItemModel()
                {
                    Id = building.Id,
                    Title = building.Title,
                    Address = building.Address,
                    Latitude = building.Latitude,
                    Longitude = building.Longitude,
                }).ToList();

            return new BuildingsViewModel()
            {
                Buildings = buildings
            };
        }

        private async Task<FloorItemModel> ConstructFloorItemModel(Guid id)
        {
            var floor = await _data.Floors.GetById(id);
            
            return new FloorItemModel()
            {
                Id = floor.Id,
                Number = floor.Number,
                Title = floor.Title,
                CreatedAtUtc = floor.CreatedAtUtc,
                SceneId = floor.SceneId,
                FloorPlanImage = floor.FloorPlanImg,
                FloorPlanGltf = floor.FloorPlanGltf
            };
        }
    }
}