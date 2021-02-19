using System;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Core.Objects;
using HseAr.Core.Settings;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.DataAccess.EFCore.DbInitializers
{
    public static class EFCoreInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var context = services.GetService<EFCoreContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var rolesManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var configuration = services.GetRequiredService<Configuration>();

            await InitializeSuperAdmin(userManager, rolesManager,configuration);
            await InitializeArClients(context, configuration);
        }
        
        private static async Task InitializeSuperAdmin(
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            Configuration configuration)
        {
            var superAdminEmail = configuration.SuperAdminEmail;
            var password = configuration.SuperAdminPassword;
            
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("admin"));
            }

            if (await roleManager.FindByNameAsync("superadmin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("superadmin"));
            }

            if (await userManager.FindByNameAsync(superAdminEmail) == null)
            {
                var admin = new User
                {
                    Email = superAdminEmail,
                    UserName = superAdminEmail,
                    Name = "SuperAdmin"
                };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "superadmin");
                }
            }
        }

        private static async Task InitializeArClients(EFCoreContext context, Configuration configuration)
        {
            foreach (var arClient in configuration.ArClients)
            {
                if (context.ArClients.FirstOrDefault(ar=> ar.Id == arClient.Id) == null)
                {
                    context.ArClients.Add( new ArClient()
                    {
                        Id = arClient.Id,
                        Url = arClient.Url,
                        ArClientType = arClient.Type.ConvertTo<ArClientType>()
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
        
    }
}