using HouseBroker.Domain.Models;
using HouseBroker.Domain.Models.Identity;
using HouseBroker.Infra.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HouseBroker.Api
{

    public static class DbSeeder
    {
        public static async Task SeedAsync(DbManagerContext context)
        {
            await context.Database.MigrateAsync();

            // Check and seed roles
            if (!context.Roles.Any())
            {
                var brokerRole = new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Description = "Broker",
                    RolePriority = 1
                };

                var seekerRole = new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Description = "Seaker", // Adjust to "Seeker" if needed
                    RolePriority = 2
                };

                await context.Roles.AddRangeAsync(brokerRole, seekerRole);
                await context.SaveChangesAsync();
            }

            // Check and seed users
            if (!context.Users.Any())
            {
                var brokerUserId = Guid.NewGuid();
                var seekerUserId = Guid.NewGuid();

                var brokerPassword = Security.Encrypt("broker@123");
                var seekerPassword = Security.Encrypt("seeker@123");

                var brokerUser = new ApplicationUser
                {
                    Id = brokerUserId,
                    UserName = "broker1",
                    Email = "broker1@example.com",
                    PasswordHash = brokerPassword,
                    IsActive = true
                };

                var seekerUser = new ApplicationUser
                {
                    Id = seekerUserId,
                    UserName = "seeker1",
                    Email = "seeker1@example.com",
                    PasswordHash = seekerPassword,
                    IsActive = true
                };

                await context.Users.AddRangeAsync(brokerUser, seekerUser);
                await context.SaveChangesAsync();

                var brokerRole = await context.Roles.FirstOrDefaultAsync(r => r.Description == "Broker");
                var seekerRole = await context.Roles.FirstOrDefaultAsync(r => r.Description == "Seaker");

                var userRoles = new[]
                {
                    new ApplicationUserRole { UserId = brokerUserId, RoleId = brokerRole.Id },
                    new ApplicationUserRole { UserId = seekerUserId, RoleId = seekerRole.Id }
                };

                await context.UserRoles.AddRangeAsync(userRoles);
                await context.SaveChangesAsync();
            }

        }
    } 
}
