using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.BusinessLayer.ArClientService
{
    public interface IArClientService
    {
        Task<string> GetStartScene(Guid floorId, Guid clientKey);

        Task<List<Building>> GetArPlaces(Guid clientKey, double? lat, double? lon );
        
        Task<Building> GetArPlaceById(Guid clientKey, Guid id);
    }
}