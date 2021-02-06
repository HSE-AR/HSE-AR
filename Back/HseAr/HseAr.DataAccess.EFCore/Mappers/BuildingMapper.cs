using System.Linq;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.DataAccess.EFCore.Mappers
{
    public class BuildingMapper : IMapper<BuildingEntity, Building>, IMapper<Building, BuildingEntity>
    {
        private readonly IMapper _mapper;
        public BuildingMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public Building Map(BuildingEntity source)
            => new Building() 
            {
                Id = source.Id,
                Title = source.Title,
                Address = source.Address,
                Coordinate = source.Coordinate,
                Floors = source.Floors.Select(floor => _mapper.Map<FloorEntity,Floor>(floor)).ToList()
            };

        public BuildingEntity Map(Building source)
            => new BuildingEntity()
            {
                Id = source.Id,
                Title = source.Title,
                Address = source.Address,
                Coordinate = source.Coordinate,
                Floors = source.Floors.Select(floor=> _mapper.Map<Floor,FloorEntity>(floor)).ToList()
            };
    }
}