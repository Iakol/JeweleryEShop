using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Infrastructure.Action
{
    public class ViewFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                var httpContext = context.HttpContext;
                var isMobileCookie = httpContext.Request.Cookies["IsMobile"];
                if (isMobileCookie != null && isMobileCookie == "true")
                {
                    viewResult.ViewName = viewResult.ViewName + ".Mobile";
                }
            }
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}
