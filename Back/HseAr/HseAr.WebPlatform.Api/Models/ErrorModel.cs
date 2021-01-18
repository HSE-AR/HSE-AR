using System.ComponentModel.DataAnnotations;
using HseAr.WebPlatform.Api.Exceptions;

namespace HseAr.WebPlatform.Api.Models
{
    public class ErrorModel
    {
        [Required]
        public WebApiErrorCode Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}