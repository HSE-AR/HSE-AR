using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Models.Floor;

namespace HseAr.WebPlatform.Api.Mappers
{
    public class FloorCreationFormMapper : IMapper<FloorCreationForm, FloorContext>
    {
        public FloorContext Map(FloorCreationForm source)
            => new FloorContext() 
            {
                Title = source.Title,
                Number = source.Number,
                BuildingId = source.BuildingId,
                FloorPlanImg = source.FloorPlanImg
            };
    }
}