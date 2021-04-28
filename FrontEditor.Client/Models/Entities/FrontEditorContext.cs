
using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FrontEditor.Client.Models.Entities
{
    public class FrontEditorContext : IdentityDbContext<User, Role, int>
    {          
        private readonly DbContextOptions _options;
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectJSON> ProjectJSONDatas { get; set; }
        
        public FrontEditorContext(DbContextOptions options) : base(options) 
        {            
            _options = options; 
         }   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);          
            modelBuilder.Entity<ProjectJSON>(entity => entity.Property(m => m.ProjectData).HasColumnType("LONGTEXT"));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.Name).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.ProviderDisplayName).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(128));

            modelBuilder.Entity<User>()
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.PhoneNumber)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.ConcurrencyStamp)
                .Ignore(c => c.TwoFactorEnabled);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            seedData(modelBuilder);            
        }       
        private void seedData(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();
            User adminUser = new User() { 
                Id = 1,  
                DisplayName = "Admin",         
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.hu",
                NormalizedEmail = "ADMIN@ADMIN.HU",
                LastActive = DateTime.Now,
                Registration = DateTime.Now,
                PasswordHash = hasher.HashPassword(null, "Admin-2021"),
                SecurityStamp = string.Empty
            };
            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<Role>().HasData(new Role() {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<Role>().HasData(new Role() {
                Id = 2,
                Name = "User",
                NormalizedName = "USER"
            });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>() {
                RoleId = 1,
                UserId = 1
            });
        }
    }   
}