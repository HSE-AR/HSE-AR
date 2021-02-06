using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.Mappers
{
    public class BuildingContextMapper : IMapper<Building,BuildingContext>, IMapper<BuildingContext,Building>
    {
        private readonly IUnitOfWork _data;
        
        public BuildingContextMapper(IUnitOfWork data)
        {
            _data = data;
        }
        
        public BuildingContext Map(Building source)
            => new BuildingContext() 
            {
                Id = source.Id,
                Title = source.Title,
                Address = source.Address,
                Coordinate = source.Coordinate,
                FloorIds = source.Floors.Select(x => x.Id).ToList()
            };
        
        public Building Map(BuildingContext source)
            => new Building() 
            {
                Id = source.Id,
                Title = source.Title,
                Address = source.Address,
                Coordinate = source.Coordinate
                //этажи не нужны здесь (при добавлении здания и при изменении здания этажи не изменяются)
            };
        
    }
}