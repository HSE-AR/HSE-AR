using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Models.Building;

namespace HseAr.WebPlatform.Api.Mappers
{
   
    public class BuildingUpdatingFormMapper : IMapper<BuildingUpdatingForm, BuildingContext>
    {
        public BuildingContext Map(BuildingUpdatingForm source)
            => new BuildingContext()
            {
                Id = source.Id,
                Title = source.Title,
                Address = source.Address,
                Latitude = source.Latitude,
                Longitude = source.Longitude,
            };
    }
}