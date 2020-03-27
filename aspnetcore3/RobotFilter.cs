using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HYJ.Formation.AspNetCore
{
    public class RobotFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Cancel) return;
            if (!(context.Result is ViewResult)) return;
            if (context.HttpContext.Response.HasStarted) return;

            context.HttpContext.Response.Headers.Add("X-Robots-Tag", "noindex");
        }
    }
}