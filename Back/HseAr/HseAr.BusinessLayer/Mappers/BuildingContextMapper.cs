using System.Linq;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.Mappers
{
    public class BuildingContextMapper : IMapper<Building,BuildingContext>, IMapper<BuildingContext,Building>
    {
        public BuildingContext Map(Building source)
            => new BuildingContext() 
            {
                Id = source.Id,
                Title = source.Title,
                Address = source.Address,
                Coordinate = source.Coordinate,
                FloorIds = source.Floors.Select(x => x.Id).ToList(),
                CompanyId = source.CompanyId
            };
        
        public Building Map(BuildingContext source)
            => new Building() 
            {
                Id = source.Id,
                Title = source.Title,
                Address = source.Address,
                Coordinate = source.Coordinate,
                CompanyId = source.CompanyId
                //этажи не нужны здесь (при добавлении здания и при изменении здания этажи не изменяются)
            };
        
    }
}