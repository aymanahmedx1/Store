using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Store.data.Context;
using Store.data.Entity.IdentityEntity;

namespace Store.WebAPI.Extentions
{
	public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,  IConfiguration _cfg)
        {
            var builder =  services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType , builder.Services);
            builder.AddEntityFrameworkStores<AppUserDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _cfg["Token:Issuer"],
                        ValidateAudience = false,    
                    };
                }); 
            return services; 
        }
    }
}
