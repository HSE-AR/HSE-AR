using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.ArClient.Api.Models;
using HseAr.Data.Entities;

namespace HseAr.ArClient.Api.ViewModelConstructor
{
    public interface IArPlacesModelConstructor
    {
        ArPlacesViewModel ConstructArPlace(List<Building> buildings);
    }
}