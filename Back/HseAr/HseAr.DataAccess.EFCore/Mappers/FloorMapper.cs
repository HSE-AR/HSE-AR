using System.Linq;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.DataAccess.EFCore.Mappers
{
 
    public class FloorMapper : IMapper<FloorEntity, Floor>, IMapper<Floor, FloorEntity>
    {
        public Floor Map(FloorEntity source)
            => new Floor() 
            {
                Id = source.Id,
                Title = source.Title,
                Number = source.Number,
                CreatedAtUtc = source.CreatedAtUtc,
                SceneId = source.SceneId,
                BuildingId = source.BuildingId
            };

        public FloorEntity Map(Floor source)
            => new FloorEntity()
            {
                Id = source.Id,
                Title = source.Title,
                Number = source.Number,
                CreatedAtUtc = source.CreatedAtUtc,
                SceneId = source.SceneId,
                BuildingId = source.BuildingId
            };
    }
}