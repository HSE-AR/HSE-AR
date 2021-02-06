using System;

namespace HseAr.WebPlatform.Api.Models.Account
{
    public class AccountViewModel
    {
        /// <summary>
        /// Индификатор
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }
    }
}