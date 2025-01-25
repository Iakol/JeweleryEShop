using Jewelery.localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Resources;

namespace Jewelery.Controllers.Shop
{
    public class ResourceHelperController : Controller
    {
        // GET: ResourceHelperController
        public JsonResult GetString(string key)
        {
            
            return Json(Resource.ResourceManager.GetString(key));
        }

        
    }
}
