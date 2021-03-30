using System;
using System.Collections.Generic;

namespace HseAr.WebPlatform.Api.Models.Building
{
    public class BuildingsViewModel
    {
        /// <summary>
        /// Список зданий, доступных пользователю
        /// </summary>
        public List<BuildingItemModel> Buildings { get; set; }

    }
    
    public class BuildingItemModel
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
        /// широта
        /// </summary>
        public double Latitude { get; set; }
        
        /// <summary>
        /// долгота
        /// </summary>
        public double Longitude { get; set; }
    }
}