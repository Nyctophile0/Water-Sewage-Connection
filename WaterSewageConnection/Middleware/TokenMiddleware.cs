using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

// Conventional Middleware class

namespace WaterSewageConnection.Middleware
{
	// install the Microsoft.AspNetCore.Http.Abstractions package into project
	// intercepts incoming requests and outgoing responses
	public class TokenMiddleware
	{
		private readonly RequestDelegate _next;

		private readonly ILogger<TokenMiddleware> _logger;

		public TokenMiddleware(RequestDelegate next, ILogger<TokenMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext httpContext)
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

			await _next(httpContext);
			//return _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class TokenMiddlewareExtensions
	{
		public static IApplicationBuilder UseTokenMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<TokenMiddleware>();
		}
	}
}
