using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.FloorService.Models;

namespace HseAr.BusinessLayer.FloorService
{
    public interface IFloorService
    {
        Task<FloorContext> CreateFloor(FloorContext floorDto, string floorPlanImg);

        Task<FloorContext> GetFloorById(Guid id);
    }
}