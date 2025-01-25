using Jewelery.data;
using Jewelery.Models;
using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.IdentityService;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web.Helpers;

namespace Jewelery.Controllers.Shop
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _db;
        private readonly IIdentityServise _identity;
        private readonly ICategoryServise _categoryService;
        private readonly IProductServise _productService;



        public HomeController(ILogger<HomeController> logger, AppDBContext db, IIdentityServise identity, ICategoryServise category, IProductServise productServise)
        {
            _logger = logger;
            _db = db;
            _identity = identity;
            _categoryService = category;
            _productService = productServise;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [Route("DeliveryAndPayment")]
        public IActionResult DeliveryAndPayment() 
        {
            return View();

        }

        public IActionResult Main(string? Sales, string? Popular, int? Page)
        {
            var CategoryList = _productService.GetAll(1);
            if (!Sales.IsNullOrEmpty())
            {
                return View(CategoryList);
            }
            if (!Popular.IsNullOrEmpty())
            {
                return View(CategoryList);
            }

            ViewData["PageCount"] = (int)Math.Ceiling((double)CategoryList.Count() / 18);

            if (Page == null) 
            {
                ViewData["CurentPage"] = 1;
            }
            else 
            {
                ViewData["CurentPage"] = Page;
            }

            int CurrentPage = (int)ViewData["CurentPage"];

            CategoryList.Skip((CurrentPage - 1) * 18).Take(18).ToList();

            return View(CategoryList);
        }

        public IActionResult SetFillterValue() 
        {
            return RedirectToAction("Main");
        
        }

        public async Task<IActionResult> Privacy()
        {
            //await _identity.AddAdmin(new RegisterDTOVM
            //{
            //    First_Name = "Admin",
            //    Second_Name = "Admin",
            //    Father_Name = "Admin",
            //    Phone_number = "380979113542",
            //    Email = "olzavra@gmail.com",
            //    UserName = "Admin",
            //    Password = "231Dima23!",
            //    ConfirmPassword = "231Dima23!"
            //});

            //await _identity.RemoveUser("bde0acd4-7ffb-4a06-a7ae-db7a69f7b5d1");
            return View();
        }

        //public IActionResult Category(int catID, int? subCat)
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

