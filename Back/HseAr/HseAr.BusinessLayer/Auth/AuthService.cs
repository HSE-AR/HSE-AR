using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Jwt;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HseAr.BusinessLayer.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtGenerator _jwt;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthService(
            IJwtGenerator jwt,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _jwt = jwt;
            _signInManager = signInManager;
        }

        public async Task<object> Login(string email, string password)
        {
            if (email == null || password == null)
                throw new Exception("Invalid login or password");

            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
                throw new Exception("Something went wrong during registration");

            var appUser = await _userManager.FindByEmailAsync(email);

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
            
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception();

            await _userManager.AddToRoleAsync(user, "admin");
            await _signInManager.SignInAsync(user, false);
            var a = await _jwt.GenerateJwt(user);
            return a;
        }
    }
}