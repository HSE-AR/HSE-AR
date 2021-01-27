using System;
using System.ComponentModel.DataAnnotations;

namespace HseAr.WebPlatform.Api.Models.Floor
{
    public class FloorCreationForm
    {
        /// <summary>
        /// Название этажа
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Номер этажа
        /// </summary>
        public int Number { get; set; }
        
        /// <summary>
        /// Id документа с облаком точек
        /// </summary>
        public Guid PointCloudId { get; set; }
        
        /// <summary>
        /// Изображение чертежа этажа
        /// </summary>
        public string FloorPlanImg { get; set; }
        
        /// <summary>
        /// Id здания
        /// </summary>
        public Guid BuildingId { get; set; }

    }
}