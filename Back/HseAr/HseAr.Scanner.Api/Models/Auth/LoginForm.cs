using System.ComponentModel.DataAnnotations;

namespace HseAr.Scanner.Api.Models.Auth
{
    public class LoginForm
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}