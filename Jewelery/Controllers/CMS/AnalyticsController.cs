using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.CMS
{
    public class AnalyticsController : Controller
    {
        public IActionResult Analytics()
        {
            return View();
        }
    }
}
