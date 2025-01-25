using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.CMS
{
    /*Контролер для выдображення статистики*/
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
