using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.Domain.Models;

namespace NewsApp.DataAccessLayer.Seeding
{
    public static class SeedAdmin
    {
        public static async Task SeedAdminAsync(this UserManager<ApplicationUser> userManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    FullName = "admin",
                    Email = "Admin@gmail.com",
                    EmailConfirmed = true,
                    UserName = "Admin",
                    PhoneNumber = "01200608207"

                };
                var result = await userManager.CreateAsync(user, "@Admin12345");
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"[SeedAdmin] Error: {error.Description}");
                    }
                }
            }
        }
    }
}
