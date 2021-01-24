using System;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HseAr.DataAccess.EFCore
{
    public class EFCoreContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<BuildingEntity> Buildings { get; set; }
        
        public DbSet<FloorEntity> Floors { get; set; }
        
        public DbSet <UserBuildingEntity> UserBuildings { get; set; }

        public EFCoreContext(DbContextOptions<EFCoreContext> opt) 
            : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBuildingEntity>()
                .HasKey(ub => new { ub.UserId, ub.BuildingId });
            
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>[]
                {
                    new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "admin",
                        NormalizedName = "ADMIN"
                    }
                });

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>[]
                {
                    new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "superadmin",
                        NormalizedName = "SUPERADMIN"
                    }
                });



            base.OnModelCreating(modelBuilder);
        }
    }
}