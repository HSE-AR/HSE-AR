using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.Mappers
{
    public class FloorContextMapper : IMapper<Floor, FloorContext>, IMapper<FloorContext, Floor>
    {
        public FloorContext Map(Floor source)
            => new FloorContext() 
            {
                Id = source.Id,
                Title = source.Title,
                Number = source.Number,
                CreatedAtUtc = source.CreatedAtUtc,
                SceneId = source.SceneId,
                BuildingId = source.BuildingId,
                FloorPlanImg = source.FloorPlanImg ,
                ImgHeight = source.ImgHeight,
                ImgWidth = source.ImgWidth
            };

        public Floor Map(FloorContext source)
            => new Floor()
            {
                Id = source.Id,
                Title = source.Title,
                Number = source.Number,
                CreatedAtUtc = source.CreatedAtUtc,
                SceneId = source.SceneId,
                BuildingId = source.BuildingId,
                FloorPlanImg = source.FloorPlanImg,
                ImgHeight = source.ImgHeight,
                ImgWidth = source.ImgWidth
            };
    }
}