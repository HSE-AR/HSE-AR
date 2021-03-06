﻿using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.BusinessLayer.Jwt
{
    public interface IJwtGenerator
    {
        Task<object> GenerateJwt(User user);
    }
}