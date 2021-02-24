using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AuthService;
using HseAr.DataAccess.EFCore.DbInitializers;

namespace HseAr.BusinessLayer
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            await EFCoreInitializer.Initialize(services);
            
            await TestDataInitializer.Initialize(services);
        }
        
    }
}