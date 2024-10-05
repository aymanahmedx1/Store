using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Store.data.Entity.IdentityEntity;

namespace Store.Repository
{
    public class AppUserContextSeed
    {
        public static async Task SeedDataAsync(UserManager<AppUser> userManager, ILoggerFactory ilogger)
        {
            try
            {
                if (!userManager.Users.Any())
                {
                    var user = new AppUser {
                     DisplayName = "Ayman Ahmed",
                     Email = "iayman8064@gmail.com",
                     UserName = "AymanX1",
                     Address = new Address { 
                        City = "Taif",
                        PostalCode="23611",
                        State="Taif",
                        Street = "Taif",
                        FirstName = "Ayman",
                        LastName= "Ahmed"
                     }
                    }; 
                    await userManager.CreateAsync(user,"Pass$0rd");    
                }
            }
            catch (Exception ex)
            {
                var logger = ilogger.CreateLogger<SeedData>();
                logger.LogError(ex.Message);
            }

        }
    }
}
