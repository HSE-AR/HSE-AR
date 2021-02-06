using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.FloorService
{
    public interface IFloorService
    {
        Task<FloorContext> CreateFloor(FloorContext floorDto);

        Task<FloorContext> GetFloorById(Guid id);
    }
}