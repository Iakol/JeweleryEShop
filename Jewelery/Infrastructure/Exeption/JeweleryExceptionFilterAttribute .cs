using Jewelery.Controllers;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Jewelery.Infrastructure.Exeption
{
    public class JeweleryExceptionFilterAttribute : ExceptionFilterAttribute 
    {

        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public JeweleryExceptionFilterAttribute(ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public override void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext exceptionContext) {
            var ExeptonType = exceptionContext.Exception;
            var routeValues = new RouteValueDictionary { { "controller", "Error" } };
            routeValues["controller"] = "Error";

            if (ExeptonType is J_BadRequestExeption)
            {
                routeValues["action"] = "BadRequestError";
            }
            else if (ExeptonType is J_NotFoundExeption)
            {
                routeValues["action"] = "NotFoundError";
            }
            else if (ExeptonType is J_ForbiddenExeption) {
                routeValues["action"] = "ForbiddenError";
            }
            else if (ExeptonType is J_ServiceTemporarilyUnavailableExeption) {
                routeValues["action"] = "ServiceTemporarilyUnavailableExeption";

            }
            else {
                routeValues["action"] = "Error";
            }



            exceptionContext.Result = new Microsoft.AspNetCore.Mvc.RedirectToRouteResult(routeValues);

            var TempData = _tempDataDictionaryFactory.GetTempData(exceptionContext.HttpContext);

            TempData["message"] = exceptionContext.Exception.Message;

            exceptionContext.ExceptionHandled = true;
        }

        
    }
}
