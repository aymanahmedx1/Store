using Microsoft.AspNetCore.Mvc;

using Store.Repository.Interface;
using Store.Repository.Repository;
using Store.Service.CashingService;
using Store.Service.HandelReponse;
using Store.Service.Helpers;
using Store.Service.ProductServices;

namespace Store.WebAPI.Extentions
{
    public static class ApllicationServcieExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICashService,CashService>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = action =>
                {
                    var errors = action.ModelState
                    .Where(m => m.Value?.Errors.Count() > 0)
                    .SelectMany(m => m.Value?.Errors)
                    .Select(m => m.ErrorMessage).ToList();
                    var responseWithErrors = new ValidationErrorResponse { Errors = errors };
                    return new BadRequestObjectResult(responseWithErrors);
                };
            });
            services.AddAutoMapper(typeof(ProductMapper));
            return services;

        }
    }
}
