using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.CMS
{
    /*Контролер в якому Встановлюємо головне фото, FAQ, Фото Ревью*/
    public class ViewController : Controller
    {
        public IActionResult SetMainImage() //посилання встановлюэться в налаштування програми appsettings
        {
            return View();
        }
    }
}
