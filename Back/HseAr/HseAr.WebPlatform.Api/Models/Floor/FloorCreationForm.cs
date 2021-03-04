using System;

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
        /// <example>data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAAAQAQMAAADOJ....</example>
        /// </summary>
        public string FloorPlanImg { get; set; }
        
        /// <summary>
        /// Id здания
        /// </summary>
        public Guid BuildingId { get; set; }

    }
}