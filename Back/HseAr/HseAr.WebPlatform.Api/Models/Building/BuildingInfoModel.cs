using System;
using System.Collections.Generic;
using HseAr.WebPlatform.Api.Models.Floor;

namespace HseAr.WebPlatform.Api.Models.Building
{
    public class BuildingInfoModel
    {
        /// <summary>
        /// Индификатор
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Адресс
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Координаты
        /// </summary>
        public string Coordinate { get; set; }
        
        /// <summary>
        /// Список этажей
        /// </summary>
        public List<FloorItemModel> Floors { get; set; }
    }
}