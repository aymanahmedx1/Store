using Microsoft.EntityFrameworkCore;

using Store.data.Context;
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
                    await context.Database.MigrateAsync();
                    await SeedData.SeedDataAsync(context, loggerFactory);
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
