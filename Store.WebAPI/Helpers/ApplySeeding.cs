using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Store.data.Context;
using Store.data.Entity.IdentityEntity;
using Store.Repository;

namespace Store.WebAPI.Helpers
{
    public class ApplySeeding
    {
        public static async Task ApplySeed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await SeedData.SeedDataAsync(context, loggerFactory);
                    await AppUserContextSeed.SeedDataAsync(userManager, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }

    }


}
