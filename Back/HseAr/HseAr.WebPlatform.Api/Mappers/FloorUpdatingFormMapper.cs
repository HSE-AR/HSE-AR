using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Models.Floor;

namespace HseAr.WebPlatform.Api.Mappers
{
    public class FloorUpdatingFormMapper : IMapper<FloorUpdatingForm, FloorContext>
    {
        public FloorContext Map(FloorUpdatingForm source)
            => new FloorContext() 
            {
                Id = source.Id,
                Title = source.Title,
                Number = source.Number,
                PointCloudId = source.PointCloudId,
                BuildingId = source.BuildingId
            };
    }
}