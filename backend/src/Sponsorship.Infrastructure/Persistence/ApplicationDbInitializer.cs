using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sponsorship.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Infrastructure.Persistence
{
    public static class ApplicationDbInitializer
    {
        public static async Task InitialiseAsync(
        IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            var userManager =
                scope.ServiceProvider
                    .GetRequiredService<
                        UserManager<ApplicationUser>>();

            var roleManager =
                scope.ServiceProvider
                    .GetRequiredService<
                        RoleManager<IdentityRole>>();

            await IdentitySeeder.SeedAsync(
                userManager,
                roleManager);
        }
    }
}
