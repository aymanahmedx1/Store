using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using StackExchange.Redis;

using Store.data.Context;
using Store.WebAPI.Extentions;
using Store.WebAPI.Helpers;
using Store.WebAPI.Middleware;

namespace Store.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); 
            builder.Services.AddDbContext<AppUserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(config => {
                return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")));
            });
           // Add services to the container.
           builder.Services.AddApplicationServices();
           builder.Services.AddIdentityService(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerConfigurations();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(op => {
                op.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("https://localhost:7061/", "http://localhost:54335");
                });
            });

            var app = builder.Build();
            await ApplySeeding.ApplySeed(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
