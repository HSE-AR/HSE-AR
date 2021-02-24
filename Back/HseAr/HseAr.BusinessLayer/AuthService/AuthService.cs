using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.CompanyService;
using HseAr.BusinessLayer.Jwt;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;

namespace HseAr.BusinessLayer.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IJwtGenerator _jwt;
        private readonly IUnitOfWork _data;
        private readonly ICompanyService _companyService;

        public AuthService(
            IJwtGenerator jwt,
            IUnitOfWork data,
            ICompanyService companyService)
        {
            _data = data;
            _jwt = jwt;
            _companyService = companyService;
        }

        public async Task<object> Login(string email, string password)
        {
            if (email == null || password == null)
{
throw new Exception("Invalid login or password");
}

var appUser = await _data.Users.FindByEmailAsync(email);

var result = await _data.Auth.CheckPasswordSignInAsync(appUser,password,false);

if (!result.Succeeded)
{
throw new Exception("Something went wrong during registration");
}

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

            await _companyService.CreateOwnCompany(user.Id, arClientId: null);
            
            return await _jwt.GenerateJwt(user);
        }
    }
}