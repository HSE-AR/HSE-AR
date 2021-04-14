using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.ArClient.Api.Models;
using HseAr.Data.Entities;

namespace HseAr.ArClient.Api.ViewModelConstructor
{
    public class ArPlacesModelConstructor : IArPlacesModelConstructor
    {
        public ArPlacesViewModel ConstructArPlaces(List<Building> buildings)
        {
            var arPlaces = new List<ArPlaceModel>();

            foreach (var building in buildings)
            {
                var arPlace = new ArPlaceModel()
                {
                    BuildingId = building.Id,
                    BuildingTitle = building.Title,
                    Latitude = building.Latitude,
                    Longitude = building.Longitude,
                    Address = building.Address,
                    CompanyId = building.CompanyId
                };
                
                arPlaces.Add(arPlace);
            }

            return new ArPlacesViewModel()
            {
                ArPlaces = arPlaces
            };
        }
        
        public ArPlaceInfoViewModel ConstructArPlaceInfo(Building building)
        {
            var arPlace = new ArPlaceModel()
            {
                BuildingId = building.Id,
                BuildingTitle = building.Title,
                Latitude = building.Latitude,
                Longitude = building.Longitude, 
                Address = building.Address, 
                CompanyId = building.CompanyId
            };

            var floors = building.Floors.Select(floor => new ArFloorModel()
            {
                FloorId = floor.Id,
                FloorNumber = floor.Number
            }).ToList();
            
            return new ArPlaceInfoViewModel()
            {
                ArPlace = arPlace,
                Floors = floors
            };
        }
    }
}