using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.Shop
{
    public class UserController : Controller
    {
        /*Контролер профілю користувача*/
        public IActionResult Index()
        {
            return View();
        }
    }
}
