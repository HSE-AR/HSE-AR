using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.FloorService;
using HseAr.Data.DataProjections;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.Models.Floor;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public class BuildingModelConstructor : IBuildingModelConstructor
    {
        private readonly IFloorService _floorService;
        
        public BuildingModelConstructor(IFloorService floorService)
        {
            _floorService = floorService;
        }
        
        public BuildingCurrentViewModel ConstructCurrentModel(BuildingContext source)
            => new BuildingCurrentViewModel() 
            {
                BuildingInfo = new BuildingInfoModel()
                {
                    Id = source.Id,
                    Title = source.Title,
                    Address  = source.Address,
                    Coordinate = source.Coordinate,
                    Floors = source.FloorIds.Select( async id => 
                        await ConstructFloorItemModel(id))
                        .Select(t => t.Result)
                        .ToList()
                }
            };
        
        public BuildingsViewModel ConstructModels(List<BuildingContext> source)
            => new BuildingsViewModel() 
            {
                Buildings = source.Select(building => 
                    new BuildingItemModel()
                    {
                        Id = building.Id,
                        Title = building.Title,
                        Address = building.Address,
                        Coordinate = building.Coordinate
                    }).ToList()
            };
        
        private async Task<FloorItemModel> ConstructFloorItemModel(Guid id)
        {
            var floorContext = await _floorService.GetFloorById(id);
            
            return new FloorItemModel()
            {
                Id = floorContext.Id,
                Number = floorContext.Number,
                Title = floorContext.Title,
                CreatedAtUtc = floorContext.CreatedAtUtc,
                SceneId = floorContext.SceneId
            };
        }
    }
}