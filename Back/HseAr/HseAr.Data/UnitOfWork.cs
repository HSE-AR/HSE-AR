using System;
using HseAr.Data.DataProjections;
using HseAr.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBuildingRepository _buildingRepo;
        private readonly IFloorRepository _floorRepo;
        private readonly ISceneRepository _sceneRepo;
        private readonly ISceneElementRepository _sceneElementRepo;
        private readonly ISceneModificationRepository _sceneModificationRepo;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UnitOfWork(
            IBuildingRepository buildingRepo,
            IFloorRepository floorRepo,
            ISceneRepository sceneRepo,
            ISceneElementRepository sceneElementRepo,
            ISceneModificationRepository sceneModificationRepo,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _buildingRepo = buildingRepo;
            _floorRepo = floorRepo;
            _sceneRepo = sceneRepo;
            _sceneElementRepo = sceneElementRepo;
            _sceneModificationRepo = sceneModificationRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IBuildingRepository Buildings => _buildingRepo;

        public IFloorRepository Floors => _floorRepo;

        public ISceneRepository Scenes => _sceneRepo;

        public ISceneElementRepository SceneElements => _sceneElementRepo;

        public ISceneModificationRepository SceneModifications => _sceneModificationRepo;

        public UserManager<User> Users => _userManager;

        public SignInManager<User> Auth => _signInManager;

        public RoleManager<IdentityRole<Guid>> Roles => _roleManager;
    }
}