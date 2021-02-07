﻿using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Jwt;
using HseAr.Data;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IJwtGenerator _jwt;
        private readonly IUnitOfWork _data;

        public AuthService(
            IJwtGenerator jwt,
            IUnitOfWork data)
        {
            _data = data;
            _jwt = jwt;
        }

        public async Task<object> Login(string email, string password)
        {
            if (email == null || password == null)
            {
                throw new Exception("Invalid login or password");
            }

            var result = await _data.Auth.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception("Something went wrong during registration");
            }

            var appUser = await _data.Users.FindByEmailAsync(email);

            return await _jwt.GenerateJwt(appUser);
        }

        public async Task<object> Register(string email, string password, string name)
        {
 
            if (email == null || password == null)
            {
                throw new Exception();
            }
            
            var user = new User()
            {
                Email = email,
                UserName = email,
                Name = name
            };
            
            var result = await _data.Users.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception();

            await _data.Users.AddToRoleAsync(user, "admin");
            await _data.Auth.SignInAsync(user, false);
            return await _jwt.GenerateJwt(user);
        }
    }
}