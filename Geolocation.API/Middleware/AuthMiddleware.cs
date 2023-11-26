using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Geolocation.API.Middleware
{
    public class AuthMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _configuration = configuration;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConst.ApiKeyHeaderName, out var requestApikey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key not found!");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConst.ApiKeySectionName);

            if (!apiKey.Equals(requestApikey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key!");
                return;
            }

            await _next(context);
        }
    }
}
