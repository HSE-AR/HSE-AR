using System.ComponentModel.DataAnnotations;

namespace HseAr.WebPlatform.Api.Models.Auth
{
    public class RegisterForm
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
        
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}