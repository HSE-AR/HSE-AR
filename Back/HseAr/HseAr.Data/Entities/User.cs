using System;
using System.Collections.Generic;
using HseAr.Data.DTO;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public ICollection<UserModelId> ModelsId { get; set; } = new List<UserModelId>();

        public User(UserDto item)
        {
            Name = item.Name;
            Email = item.Email;
            UserName = item.Email;
        }
        public User()
        {

        }
    }
}