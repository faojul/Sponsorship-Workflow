using Microsoft.AspNetCore.Identity;
using Sponsorship.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[]
            {
                Roles.Requestor,
                Roles.Manager,
                Roles.FinanceAdmin,
                Roles.SystemAdmin
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            await CreateUserAsync(userManager, "requestor@test.com", "Requestor User", Roles.Requestor);
            await CreateUserAsync(userManager, "manager@test.com", "Manager User", Roles.Manager);
            await CreateUserAsync(userManager, "finance@test.com", "Finance User", Roles.FinanceAdmin);
            await CreateUserAsync(userManager, "admin@test.com", "System Admin", Roles.SystemAdmin);
        }

        private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager, string email, string fullName, string role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
                return;

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Test123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
