using Jewelery.data;
using Jewelery.Infrastructure.Enums;
using Jewelery.Models.Product_model;
using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.FillterSession;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace Jewelery.Controllers.Shop
{
    public class ShopController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IProductServise _productServise;
        private readonly ICategoryServise _categoryServise;
        private readonly ISubCategoryServise _subCategoryServise;
        private readonly CategoryConstantDisplayService _categoryConstantDisplayService;
        private const string SessionKey = "Fillter";



        public ShopController(AppDBContext db, IProductServise productServise, ICategoryServise categoryServise, ISubCategoryServise subCategoryServise, CategoryConstantDisplayService categoryConstantDisplayService)
        {
            _db = db;
            _productServise = productServise;
            _categoryServise = categoryServise;
            _subCategoryServise = subCategoryServise;
            _categoryConstantDisplayService = categoryConstantDisplayService;
        }

        [Route("Shop/Main")]
        public IActionResult Main()
        {
            return View();

        }
        public IActionResult Index(int? curentPage , string? Constanta)
        {

            //initiate variable
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            List<ProductDTOVMPage> list = new List<ProductDTOVMPage>();
            var Config = _categoryConstantDisplayService.GetDisplaySettings();
            CategorySessionFilterObject fillter;

            //Condition that Category the same
            if (HttpContext.Session.GetString(SessionKey).IsNullOrEmpty())
            {
                fillter = new CategorySessionFilterObject();
                fillter.Constanta = Constanta;
            }
            else
            {
                fillter = JsonSerializer.Deserialize<CategorySessionFilterObject>(HttpContext.Session.GetString(SessionKey));

                if (fillter.CategoryId == null && fillter.SubCategoryId == null && (!Constanta.IsNullOrEmpty()? !fillter.Constanta.IsNullOrEmpty() ? fillter.Constanta.Equals(Constanta) : false : fillter.Constanta.IsNullOrEmpty()))
                {
                    if (fillter.ListChangeFlag)
                    {
                        fillter.ListChangeFlag = false;
                        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(fillter));
                        RedirectToAction(nameof(Index), new { Constanta = Constanta });
                    }
                    else
                    {
                        if (fillter.currentPage == curentPage)
                        {

                        }
                        else
                        {
                            fillter.currentPage = curentPage;
                        }
                    }
                }
                else
                {
                    fillter = new CategorySessionFilterObject();
                    fillter.Constanta = Constanta;
                }
            }




            if (Constanta.IsNullOrEmpty())
            {
                list = _productServise.GetAll(CultureInt).ToList();
                fillter.SubCategoryList = _categoryServise.GetAll(CultureInt);

            }
            else if(Constanta.Equals("Sales"))
            {
                list = _productServise.GetAll(CultureInt).ToList().Where(p => p.isPromotion).ToList();
            }
            else if (Constanta.Equals("Sertificat"))
            {
                list = _productServise.GetAll(CultureInt).ToList().Where(p => p.Category_id == Config.CategoryDisplay.Sertificat).ToList();
            }
            else 
            {
                list = _productServise.GetAll(CultureInt).ToList();
                fillter.SubCategoryList = _categoryServise.GetAll(CultureInt);
            }

            //FILTERING
            //Is exist filter
            if (fillter.isExist != null) 
            {            
                list = list.Where(p => p.isExist == fillter.isExist).ToList();
            }


            //Set Min and Max Price of list
            if (list.Count > 0)
            {
                fillter.MaxPrice = list.Max(p => p.Price);
                fillter.MinPrice = list.Min(p => p.Price);
            }
            //Cheack if filter Price range is`t out of board max and min price in list
            if ((fillter.SelectMaxPrice > fillter.MaxPrice || fillter.SelectMinPrice < fillter.MinPrice) || (fillter.SelectMaxPrice == null || fillter.SelectMinPrice == null))
            {
                fillter.SelectMaxPrice = fillter.MaxPrice;
                fillter.SelectMinPrice = fillter.MinPrice;
            }
            //FILTERING
            //SetPriceRange
            list = list.Where(p => p.Price >= fillter.SelectMinPrice && p.Price <= fillter.SelectMaxPrice).ToList();

            //FILTERING
            //Set Ordering
            switch (fillter.OrderSelect)
            {
                case 0:
                    list = list.OrderBy(p => p.Created_at).ToList();
                    break;
                case 1:
                    list = list.OrderBy(p => p.Price).ToList();
                    break;
                case 2:
                    list = list.OrderByDescending(p => p.Price).ToList();
                    break;
            }

            fillter.PageCount = (int)Math.Ceiling(list.Count() / (double)18);

            if (fillter.currentPage != null && fillter.currentPage != 0)
            {
                list = list
                .Skip((int)((curentPage - 1) * 18))
                .Take(18)
                .ToList();

            }
            else 
            {
                list = list
                .Take(18)
                .ToList();
            }

            //set Filter to Session
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(fillter));
            return View(list);

        }



        //[Route("Shop/Category/{cat_id:int}/{sub_cat_id:int?}")]
        public IActionResult Category(int cat_id, int? sub_cat_id, int? curentPage)
        {
            //Initiate value
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            List<ProductDTOVMPage> list = new List<ProductDTOVMPage>();

            CategorySessionFilterObject fillter;

            //Condition that Category the same
            if (HttpContext.Session.GetString(SessionKey).IsNullOrEmpty())
            {
                fillter = new CategorySessionFilterObject();
            }
            else 
            {
                fillter = JsonSerializer.Deserialize<CategorySessionFilterObject>(HttpContext.Session.GetString(SessionKey));

                if (fillter.CategoryId == cat_id && fillter.SubCategoryId == sub_cat_id && fillter.Constanta.IsNullOrEmpty())
                {
                    if (fillter.ListChangeFlag)
                    {
                        fillter.ListChangeFlag = false;
                        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(fillter));
                        RedirectToAction(nameof(Category), new { cat_id = cat_id, sub_cat_id = sub_cat_id });
                    }
                    else 
                    {
                        if (fillter.currentPage == curentPage)
                        {

                        }
                        else
                        {
                            fillter.currentPage = curentPage;
                        }
                    }                    
                }
                else 
                {
                    fillter = new CategorySessionFilterObject(); 
                }
            }

            //Get List of Products and add Categories Filer
            if (sub_cat_id != null && sub_cat_id != 0)
            {
                list = _productServise.GetAll(CultureInt).Where(p => p.SubCategory_id == sub_cat_id).ToList();
                fillter.CategoryName = _categoryServise.GetById(cat_id, 1).Name;
                fillter.SubCategoryName = _subCategoryServise.GetByCategory(1, cat_id)[0].Name;
                fillter.CategoryId = _categoryServise.GetById(cat_id, 1).Category_id;
                fillter.SubCategoryId = sub_cat_id;

            }
            else 
            {
                list = _productServise.GetAll(CultureInt).Where(p => p.Category_id == cat_id).ToList();
                fillter.CategoryName = _categoryServise.GetById(cat_id, 1).Name;
                fillter.CategoryId = _categoryServise.GetById(cat_id, 1).Category_id;
            }

            //Add SubCategoryList for filters in View
            fillter.SubCategoryList = _subCategoryServise.GetByCategory(CultureInt, cat_id).ToList();

            //FILTERING
            //Is exist filter
            if (fillter.isExist != null)
            {
                list = list.Where(p => p.isExist == fillter.isExist).ToList();



            }



            //Set Min and Max Price of list
            if (list.Count > 0)
            {
                fillter.MaxPrice = list.Max(p => p.Price);
                fillter.MinPrice = list.Min(p => p.Price);
            }
            //Cheack if filter Price range is`t out of board max and min price in list
            if((fillter.SelectMaxPrice > fillter.MaxPrice || fillter.SelectMinPrice < fillter.MinPrice)|| (fillter.SelectMaxPrice == null || fillter.SelectMinPrice == null)) 
            {
                fillter.SelectMaxPrice = fillter.MaxPrice;
                fillter.SelectMinPrice = fillter.MinPrice; 
            }
            //FILTERING
            //SetPriceRange
            list = list.Where(p => p.Price >= fillter.SelectMinPrice && p.Price <= fillter.SelectMaxPrice).ToList();

            //FILTERING
            //Set Ordering
            switch (fillter.OrderSelect)
            {
                case 0:
                    list = list.OrderBy(p => p.Created_at).ToList();
                    break;
                case 1:
                    list = list.OrderBy(p => p.Price).ToList();
                    break;
                case 2:
                    list = list.OrderByDescending(p => p.Price).ToList();
                    break;
            }

            //Get list for page and set Page Count
            fillter.PageCount = (int)Math.Ceiling(list.Count() / (double)18);
            if (fillter.currentPage != null && fillter.currentPage != 0)
            {
                list = list
                .Skip((int)((fillter.currentPage - 1) * 18))
                .Take(18)
                .ToList();

            }
            else
            {
                list = list
                .Take(18)
                .ToList();
            }

            //set Filter to Session
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(fillter));
            return View(list);

        }

        [HttpPost]
        public IActionResult SetFillterValue([FromBody] FilterDTO AddFilter) 
        {
            CategorySessionFilterObject fillter = new CategorySessionFilterObject();


            if (HttpContext.Session.GetString(SessionKey).IsNullOrEmpty())
            {
                fillter = new CategorySessionFilterObject();
            }
            else 
            {
                fillter = JsonSerializer.Deserialize<CategorySessionFilterObject>(HttpContext.Session.GetString(SessionKey));

            }
            switch (AddFilter.fillerid) {
                case 0:
                    int OrderSelect = JsonSerializer.Deserialize<OrderSelectFilterDTO>(AddFilter.additionalparameter).OrderString;
                    switch (OrderSelect) 
                    {
                        case 0:
                            fillter.OrderSelect = 0;
                            break;
                        case 1:
                            fillter.OrderSelect = 1;
                            break;
                        case 2:
                            fillter.OrderSelect = 2;
                            break;
                    }                   
                    break;
                case 1:
                    fillter.isExist = JsonSerializer.Deserialize<IsExistFilterDTO>(AddFilter.additionalparameter).IsExist;
                    fillter.ListChangeFlag = true;
                    break;
                case 2:
                    fillter.SelectMaxPrice = JsonSerializer.Deserialize<PriceRangeFilterDTO>(AddFilter.additionalparameter).SelectMaxPrice;
                    fillter.SelectMinPrice = JsonSerializer.Deserialize<PriceRangeFilterDTO>(AddFilter.additionalparameter).SelectMinPrice;
                    fillter.ListChangeFlag = true;
                    break;
            }
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(fillter));
            return null;
        }

        public IActionResult Product(int id) 
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));

            var item = _productServise.GetById(id, CultureInt);

            if (item.SubCategory_id != null && item.SubCategory_id != 0)
            {
                ViewData["Category"] = _categoryServise.GetById(item.Category_id, CultureInt).Name;
                ViewData["SubCategory"] = _subCategoryServise.GetByCategory(CultureInt, item.Category_id).FirstOrDefault(i => i.Category_id == item.SubCategory_id).Name;
            }
            else
            {
                ViewData["Category"] = _categoryServise.GetById(item.Category_id, CultureInt).Name;
            }
            return View(item);
        }

        [Route("Shop/WishList")]
        public IActionResult WishList()
        {

            return View();
        }

        [Route("Shop/Search")]
        public IActionResult Search()
        {

            return View();
        }

        [HttpPost]
        [Route("Shop/Search")]
        public IActionResult WishList(string searchquerry)
        {

            return View();
        }
    }
}
