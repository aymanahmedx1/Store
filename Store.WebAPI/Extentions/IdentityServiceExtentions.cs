using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Store.data.Context;
using Store.data.Entity.IdentityEntity;

namespace Store.WebAPI.Extentions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            var builder =  services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType , builder.Services);
            builder.AddEntityFrameworkStores<AppUserDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(); 
            return services; 
        }
    }
}
