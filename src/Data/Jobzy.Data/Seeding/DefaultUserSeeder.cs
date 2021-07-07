namespace Jobzy.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class DefaultUserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedDefaultUserAsync(userManager);
        }

        private static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                var admin = new Administrator
                {
                    Name = "Stoyan Kostadinov",
                    UserName = "tonsan1",
                    Email = "admin@admin.com",
                };

                var result = await userManager.CreateAsync(admin, "123456");

                if (result.Succeeded)
                {
                   await userManager.AddToRoleAsync(admin, "Administrator");
                }
            }
        }
    }
}
