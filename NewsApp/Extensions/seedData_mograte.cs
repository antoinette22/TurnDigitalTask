using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.DataAccessLayer.Data;
using NewsApp.DataAccessLayer.Seeding;
using NewsApp.Domain.Models;

namespace NewsApp.Extensions
{
    public static class seedData_mograte
    {
        public async static Task seedData(WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var _dbContext = services.GetRequiredService<ApplicationDbContext>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                //Seeding Roles
                await services.SeedAsync();
                //Seeding Admin
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await userManager.SeedAdminAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during applying the migrations or Seeding Data");
            }
        }
    }
}
