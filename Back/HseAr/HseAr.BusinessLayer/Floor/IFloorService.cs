using System;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.Floor
{
    public interface IFloorService
    {
        Task<Data.DataProjections.Floor> CreateFloor(Data.DataProjections.Floor floorDto);
    }
}