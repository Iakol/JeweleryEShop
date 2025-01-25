using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Jewelery.Infrastructure.Exeption
{
    public class JeweleryExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        

        public JeweleryExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            if(context.Response.StatusCode == 404) {

                context.Response.Redirect("/Error/Error?Message=NotFound");

            }
            else if (context.Response.StatusCode == 400)
            {
                context.Response.Redirect("/Error/Error?Message=BadRequest");

            }
            else if (context.Response.StatusCode == 403)
            {
                context.Response.Redirect("/Error/Error?Message=Forbidden");

            }
            else if (context.Response.StatusCode == 503)
            {
                context.Response.Redirect("/Error/Error?Message=ServiceTemporarilyUnavailableExeption");

            }
            else if (context.Response.StatusCode >= 400)
            {
                context.Response.Redirect("/Error/Error?Message=Global");

            }
            else {
            }
        }
    }
}
