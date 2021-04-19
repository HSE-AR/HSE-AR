using System;

#nullable enable
namespace HseAr.WebPlatform.Api.Models.Building
{
    public class BuildingUpdatingForm
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Address { get; set; }
        
        /// <summary>
        /// широта
        /// </summary>
        public double Latitude { get; set; }
        
        /// <summary>
        /// долгота
        /// </summary>
        public double Longitude { get; set; }
        
        /// <summary>
        /// Изображение здания
        /// <example>data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAAAQAQMAAADOJ....</example>
        /// </summary>
        public string ImgBase64 { get; set; }
    }
}