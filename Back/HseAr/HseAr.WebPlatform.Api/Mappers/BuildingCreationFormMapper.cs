using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Models.Building;

namespace HseAr.WebPlatform.Api.Mappers
{
    public class BuildingCreationFormMapper : IMapper<BuildingCreationForm, BuildingContext>
    {
        public BuildingContext Map(BuildingCreationForm source)
            => new BuildingContext()
            {
                Title = source.Title,
                Address = source.Address,
                Coordinate = source.Coordinate
            };
    }
}