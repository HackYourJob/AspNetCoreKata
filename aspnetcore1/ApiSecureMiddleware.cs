using System.Linq;
using System.Threading.Tasks;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Http;

namespace HYJ.Formation.AspNetCore
{
    public class ApiSecureMiddleware
    {
        private const string TokenPrefix = "Bearer";
        
        private readonly RequestDelegate _next;

        public ApiSecureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiKeyStore apiKeyStore)
        {
            if (!apiKeyStore.IsAllowed(ExtractApiKey(context)))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("forbidden");
                return;
            }

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