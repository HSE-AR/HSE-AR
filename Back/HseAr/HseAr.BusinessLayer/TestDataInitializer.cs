using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Afisha.Tickets.Core.Linq;
using HseAr.BusinessLayer.AuthService;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.CompanyService;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.BusinessLayer.FloorService;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.PointCloudService;
using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
using HseAr.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HseAr.BusinessLayer
{
    public static class TestDataInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var configuration = services.GetService<Configuration>();
            var data = services.GetService<IUnitOfWork>();
            var authService = services.GetService<IAuthService>();
            var buildingService = services.GetService<IBuildingService>();
            var companyService = services.GetService<ICompanyService>();
            var floorService = services.GetService<IFloorService>();
            var pcdService = services.GetService<IPointCloudService>();
            
            var user = await InitializeTestUser(data, configuration);
            
            var ownCompany = await InitializeTestOwnCompany(user, companyService);

            var buildingInfo = await InitializerTestBuilding(ownCompany, data);

            var pcd = await InitializePointCloud(ownCompany, pcdService, configuration);
            
            var floor = await InitializerTestFloor(buildingInfo, pcd, floorService, configuration);
        }

        private static async Task<User> InitializeTestUser(IUnitOfWork data, Configuration configuration)
        {
            if(await data.Users.FindByEmailAsync(configuration.TestUserEmail) == null)
            {
                var testUser = new User
                {
                    Email = configuration.TestUserEmail,
                    UserName = configuration.TestUserEmail,
                    Name = "TestUser"
                };
                var result = await data.Users.CreateAsync(testUser, configuration.TestUserPassword);
                if (result.Succeeded)
                {
                    await data.Users.AddToRoleAsync(testUser, "admin");
                }
            }
            
            return await data.Users.FindByEmailAsync(configuration.TestUserEmail);
        }

        private static async Task<CompanyContext> InitializeTestOwnCompany(User user, ICompanyService companyService)
        {
            var companies = await companyService.GetCompaniesByUserId(user.Id);

            var hasNotTestOwnCompany = companies.IsNullOrEmpty() || !companies.Any(x => x.TariffPlan == TariffPlanType.OwnTariff);
            
            return hasNotTestOwnCompany 
                ? await companyService.CreateOwnCompany(user.Id, arClientId: null)
                : companies.FirstOrDefault(c => c.TariffPlan == TariffPlanType.OwnTariff);
        }

        private static async Task<Building> InitializerTestBuilding(CompanyContext ownCompany, IUnitOfWork data)
        {
            Building buildingTest;
            var buildings = await data.Buildings.GetListByCompanyId(ownCompany.Id);
            
            if (buildings.IsNullOrEmpty())
            {
                var building = new Building()
                {
                    CompanyId = ownCompany.Id,
                    Title = "Test Title",
                    Address = "Test Address",
                    Latitude = 55.6147603,
                    Longitude = 37.6001614,
                };
                
                buildingTest = await data.Buildings.Add(building);
            }
            else
            {
                buildingTest = buildings.First();
            }

            return await data.Buildings.GetById(buildingTest.Id);
        }

        private static async Task<PointCloudContext> InitializePointCloud(CompanyContext company, IPointCloudService pcdService, Configuration configuration)
        {
            var pcds = await pcdService.GetPointClouds(company.Id);
            if (pcds.IsNullOrEmpty())
            {
                var pcdPath = $"{configuration.STORAGE_PATH}/pointclouds/testData.xyz";
                using var stream = new MemoryStream(File.ReadAllBytes(pcdPath).ToArray());
                var formFile = new FormFile(stream, 0, stream.Length, "streamFile", pcdPath.Split(@"\").Last());

                var pcd = new PointCloudContext()
                {
                    Name = "TestPcd"
                };

                return await pcdService.AddPointCloudToCompany(pcd, formFile, company.Id);
            }

            return pcds.First();
        }
        
        private static async Task<FloorContext> InitializerTestFloor(
            Building buildingInfo,
            PointCloudContext pcd,
            IFloorService floorService,
            Configuration configuration)
        {
            if (buildingInfo.Floors.IsNullOrEmpty())
            {
                var floor = new FloorContext()
                {
                    Number = 1,
                    Title = "Test Title Floor",
                    CreatedAtUtc = DateTime.Now,
                    BuildingId = buildingInfo.Id,
                    PointCloudId = pcd.Id
                };
                
                var imagePath = $"{configuration.STORAGE_PATH}/floorplans/imgs/testData.jpg";
                using (var image = Image.FromFile(imagePath))
                {
                    using (var m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        var imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                        var base64String = Convert.ToBase64String(imageBytes);
                        return await floorService.CreateFloor(floor, base64String, buildingInfo.CompanyId);
                        
                    }
                }
            }

            return await floorService.GetFloorById(buildingInfo.Floors.First().Id, buildingInfo.CompanyId);
        }
    }
}