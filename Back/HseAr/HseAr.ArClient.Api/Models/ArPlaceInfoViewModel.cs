using System.Collections.Generic;

namespace HseAr.ArClient.Api.Models
{
    public class ArPlaceInfoViewModel
    {
        public ArPlaceModel ArPlace { get; set; }
        
        public List<ArFloorModel> Floors { get; set; }
    }
}