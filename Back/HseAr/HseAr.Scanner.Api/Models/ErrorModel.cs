using System.ComponentModel.DataAnnotations;
using HseAr.Scanner.Api.Exceptions;

namespace HseAr.Scanner.Api.Models
{
    public class ErrorModel
    {
        [Required]
        public ScannerApiErrorCode Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}