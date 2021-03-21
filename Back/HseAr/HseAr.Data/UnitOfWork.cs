using System;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBuildingRepository _buildingRepo;
        private readonly IFloorRepository _floorRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly IPositionRepository _positionRepo;
        private readonly IArClientRepository _arClientRepo;
        private readonly ISceneRepository _sceneRepo;
        private readonly IPointCloudRepository _pointCloudRepo;
        private readonly HseArUserManager _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UnitOfWork(
            IBuildingRepository buildingRepo,
            IFloorRepository floorRepo,
            ICompanyRepository companyRepo,
            IPositionRepository positionRepo,
            IArClientRepository arClientRepo,
            ISceneRepository sceneRepo,
            IPointCloudRepository pointCloudRepo,
            HseArUserManager userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _buildingRepo = buildingRepo;
            _floorRepo = floorRepo;
            _companyRepo = companyRepo;
            _positionRepo = positionRepo;
            _arClientRepo = arClientRepo;
            _sceneRepo = sceneRepo;
            _pointCloudRepo = pointCloudRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IBuildingRepository Buildings => _buildingRepo;

        public IFloorRepository Floors => _floorRepo;

        public ISceneRepository Scenes => _sceneRepo;

        public ICompanyRepository Companies => _companyRepo;

        public IPositionRepository Positions => _positionRepo;

        public IArClientRepository ArClients => _arClientRepo;

        public IPointCloudRepository PointClouds => _pointCloudRepo;

        public HseArUserManager Users => _userManager;

        public SignInManager<User> Auth => _signInManager;

        public RoleManager<IdentityRole<Guid>> Roles => _roleManager;
    }
}