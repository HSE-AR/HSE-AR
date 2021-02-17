using System;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data
{
    public interface IUnitOfWork
    {
        IBuildingRepository Buildings { get; }
        
        IFloorRepository Floors { get; }
        
        ISceneRepository Scenes { get; }
        
        UserManager<User> Users { get; }
        
        SignInManager<User> Auth { get; }
        
        RoleManager<IdentityRole<Guid>> Roles { get; }
    }
}