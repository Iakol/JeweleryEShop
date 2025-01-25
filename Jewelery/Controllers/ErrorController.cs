using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jewelery.Controllers
{
    public class ErrorController : Controller
    {

        public IActionResult Error()
        {
            return View("~/Views/Error/Error.cshtml");
        }
        public IActionResult NotFoundError()
        {
            return View("~/Views/Error/NotFound.cshtml");
        }
        public IActionResult BadRequestError()
        {
            return View("~/Views/Error/BadRequest.cshtml");
        }
        public IActionResult ForbiddenError()
        {
            return View("~/Views/Error/Forbidden.cshtml");
        }
        public IActionResult ServiceTemporarilyUnavailableExeptionError()
        {
            return View("~/Views/Error/ServiceTemporarilyUnavailableExeption.cshtml");
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            if (HttpContext.Request.Query.ContainsKey("message"))
            {
                if (HttpContext.Request.Query["message"].Equals("NotFound"))
                {
                    TempData["message"] = JsonConvert.SerializeObject(new J_NotFoundExeption());
                    context.Result = RedirectToAction(nameof(NotFoundError));
                }
                else if (HttpContext.Request.Query["message"].Equals("BadRequest"))
                {
                    TempData["message"] = JsonConvert.SerializeObject(new J_BadRequestExeption());
                    context.Result = RedirectToAction(nameof(BadRequestError));
                }
                else if (HttpContext.Request.Query["message"].Equals("Forbidden"))
                {
                    TempData["message"] = JsonConvert.SerializeObject(new J_ForbiddenExeption());
                    context.Result = RedirectToAction(nameof(ForbiddenError));
                }
                else if (HttpContext.Request.Query["message"].Equals("ServiceTemporarilyUnavailableExeption"))
                {
                    TempData["message"] = JsonConvert.SerializeObject(new J_ServiceTemporarilyUnavailableExeption());
                    context.Result = RedirectToAction(nameof(ServiceTemporarilyUnavailableExeptionError));
                }
                else
                {
                    ViewBag.ErrorMessage = new Exception().Message;

                }

            }
            else {

                ViewBag.ErrorMessage = TempData["message"]; 
            }

        }
    }
}
