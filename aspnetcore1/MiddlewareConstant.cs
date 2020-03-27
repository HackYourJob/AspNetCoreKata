using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HYJ.Formation.AspNetCore
{
    public class MiddlewareConstant
    {
        private readonly RequestDelegate _next;

        public MiddlewareConstant(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<MiddlewareConstant> logger, IOptions<MiddlewareConstantOptions> options)
        {
            logger.LogDebug(context.Request.Headers["user-agent"]);
            logger.LogDebug(context.Request.Path);
            
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(options.Value.Answer);
        }
    }
}