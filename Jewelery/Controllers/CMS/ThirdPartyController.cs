using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.CMS
{
    /*Контролер в якому встановлюємо налаштування для АПІ 3 сторін*/
    public class ThirdPartyController : Controller
    {
        private readonly IConfiguration _configuration;

        public ThirdPartyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult NovaPost()
        {
            string Key = _configuration["NovaPostConection:ApiKey"];
            return View(model:Key);
        }
        [HttpPost]
        public IActionResult NovaPost(string key)
        {
            _configuration["NovaPostConection:ApiKey"] = key;
            return RedirectToAction(nameof(NovaPost));
        }
        public IActionResult MonoPay()
        {
            string Key = _configuration["MonoPayCoection:ApiKey"];
            return View(model:Key);
        }

        [HttpPost]
        public IActionResult MonoPay(string key)
        {
            _configuration["MonoPayCoection:ApiKey"] = key;
            return RedirectToAction(nameof(MonoPay));
        }
    }
}
