using Microsoft.OpenApi.Models;

namespace Store.WebAPI.Extentions
{
	public static class SwaggerConfigurations
	{
		public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(
					"v1",
					new OpenApiInfo
					{
						Title = "Store Api",
						Version = "v1",
						Contact = new OpenApiContact
						{
							Email = "Iayman8064@gmail.com",
							Name = "Ayman Ahmed",
							Url = new Uri("https://github.com/aymanahmedx1/Store")
						}
					}
					);
				var security = new OpenApiSecurityScheme
				{
					Description = "JWT",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "bearer",
					Reference = new OpenApiReference
					{
						Id = "bearer",
						Type = ReferenceType.SecurityScheme,
					}
				};
				c.AddSecurityDefinition("bearer", security);
				var securityRequirments = new OpenApiSecurityRequirement {
					{ security , new [] {"bearer" } }
				};
				c.AddSecurityRequirement(securityRequirments);
			});
			return services;
		}
	}
}