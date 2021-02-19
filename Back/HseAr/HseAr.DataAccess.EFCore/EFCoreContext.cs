using System;
using HseAr.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HseAr.DataAccess.EFCore
{
    public sealed class EFCoreContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Building> Buildings { get; set; }
        
        public DbSet<Floor> Floors { get; set; }
        
        public DbSet<Position> Positions { get; set; }
        
        public DbSet<Company> Companies { get; set; }
        
        public DbSet<ArClient> ArClients { get; set; }

        public EFCoreContext(DbContextOptions<EFCoreContext> opt) 
            : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>()
                .HasKey(uc => new { uc.UserId, uc.CompanyId });

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