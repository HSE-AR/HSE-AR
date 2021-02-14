using System.ComponentModel.DataAnnotations;
using HseAr.ArClient.Api.Exceptions;

namespace HseAr.ArClient.Api.Models
{
    public class ErrorModel
    {
        [Required]
        public ArClientApiErrorCode Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}