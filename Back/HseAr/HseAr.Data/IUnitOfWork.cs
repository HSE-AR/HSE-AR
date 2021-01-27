using System;
using HseAr.Data.DataProjections;
using HseAr.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data
{
    public interface IUnitOfWork
    {
        IBuildingRepository Buildings { get; }
        
        IFloorRepository Floors { get; }
        
        ISceneRepository Scenes { get; }
        
        ISceneElementRepository SceneElements { get; }
        
        ISceneModificationRepository SceneModifications { get; }
        
        UserManager<User> Users { get; }
        
        SignInManager<User> Auth { get; }
        
        RoleManager<IdentityRole<Guid>> Roles { get; }
    }
}