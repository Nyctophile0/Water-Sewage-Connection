using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WaterSewageConnection.Middleware
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            var token = httpContext.Request.Cookies["jwtToken"];

            if (!string.IsNullOrEmpty(token))
            {
                //_logger.LogInformation("Jwt token found in cookie");
                httpContext.Request.Headers.Add("Authorization", $"Bearer {token}");
            }
            //else
            //{
            //	_logger.LogWarning("Not found");
            //}

            await next(httpContext);
        }


        
    }

    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder CustomMiddlewareExtensionMethod(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddleware>();
        }
    }
}
