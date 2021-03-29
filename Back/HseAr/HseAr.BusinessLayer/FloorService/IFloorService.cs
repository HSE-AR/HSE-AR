using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.FloorService.Models;

namespace HseAr.BusinessLayer.FloorService
{
    public interface IFloorService
    {
        Task<FloorContext> CreateFloor(FloorContext floorDto, string floorPlanImg, Guid companyId);

        Task<FloorContext> GetFloorById(Guid id, Guid companyId);

        Task DeleteFloor(Guid id, Guid companyId);
        
    }
}