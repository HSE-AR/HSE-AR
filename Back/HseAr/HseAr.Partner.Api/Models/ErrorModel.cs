using System.ComponentModel.DataAnnotations;
using HseAr.Partner.Api.Exceptions;

namespace HseAr.Partner.Api.Models
{
    public class ErrorModel
    {
        [Required]
        public PartnerApiErrorCode Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}