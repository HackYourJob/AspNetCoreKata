using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GrpcHost
{
    public class ApiSecureMiddleware
    {
        private const string TokenPrefix = "Bearer";
        
        private readonly RequestDelegate _next;

        public ApiSecureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var apiKey = ExtractApiKey(context);
            if(apiKey != "Toto")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("forbidden");
                return;
            }
            
            context.User = new GenericPrincipal(new GenericIdentity("toto"), new string[0]);

            await _next(context);
        }
        
        private static string ExtractApiKey(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var header))
            {
                return header
                    .Where(h => h.StartsWith(TokenPrefix))
                    .Select(h => h.Replace(TokenPrefix, "").Trim())
                    .FirstOrDefault() ?? "";
            }

            return "";
        }
    }
}