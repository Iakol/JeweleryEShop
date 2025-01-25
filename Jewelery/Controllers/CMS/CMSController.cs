using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Jewelery.Controllers.CMS
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CMSController : Controller
    {
        public IActionResult CMSMain()

        {
           return View();
        }

        public IActionResult LoadSection(string section)
        {
            switch (section) {
                case "ProductCMS":
                    return PartialView("CMSProductMain.cshtml");
                case "CategoryCMS":
                    return View();
                case "UserCMS":
                    return View();
                case "OrderCMS":
                    return View();
                case "AnaliticCMS":
                    return View();
                case "NovaPost":
                    return View();
            }

            throw new J_BadRequestExeption();


        }
    }
}
