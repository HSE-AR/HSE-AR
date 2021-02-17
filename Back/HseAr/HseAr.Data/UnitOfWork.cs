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
        private readonly ISceneRepository _sceneRepo;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UnitOfWork(
            IBuildingRepository buildingRepo,
            IFloorRepository floorRepo,
            ISceneRepository sceneRepo,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _buildingRepo = buildingRepo;
            _floorRepo = floorRepo;
            _sceneRepo = sceneRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IBuildingRepository Buildings => _buildingRepo;

        public IFloorRepository Floors => _floorRepo;

        public ISceneRepository Scenes => _sceneRepo;

        public UserManager<User> Users => _userManager;

        public SignInManager<User> Auth => _signInManager;

        public RoleManager<IdentityRole<Guid>> Roles => _roleManager;
    }
}