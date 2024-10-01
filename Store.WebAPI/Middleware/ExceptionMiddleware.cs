using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Http;

using Store.Service.HandelReponse;

namespace Store.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            IHostEnvironment environment,
            ILogger<ExceptionMiddleware> logger
            )
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var respnse = _environment.IsDevelopment()
                    ? new CustomException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) :
                    new CustomException((int)HttpStatusCode.InternalServerError);
                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(respnse, option);   
                await context.Response.WriteAsync(json);    
            }
        }
    }
}
