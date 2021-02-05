using System;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.FloorService
{
    public interface IFloorService
    {
        Task<Floor> CreateFloor(Floor floorDto);
    }
}