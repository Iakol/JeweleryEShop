using Jewelery.data;
using Jewelery.Models.Product_model;
using Jewelery.Servise.CategoryServise;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Newtonsoft.Json;
using Jewelery.ViewModels.DTO.SubCategory;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace Jewelery.Controllers.CMS
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly AppDBContext _db;
        private readonly ICategoryServise _categoryServise;
        private readonly ISubCategoryServise _SubcategoryServise;
        private readonly CategoryConstantDisplayService _categoryConstantDisplayService;

        public CategoryController(AppDBContext db, ICategoryServise categoryServise
            , ISubCategoryServise SubcategoryServise, CategoryConstantDisplayService categoryConstantDisplayService)
        {
            _db = db;
            _categoryServise = categoryServise;
            _SubcategoryServise = SubcategoryServise;
            _categoryConstantDisplayService = categoryConstantDisplayService;
        }

        public IActionResult Category()
        { 
            return View();
        }

        public IActionResult UpdateViewOrder()
        {
            
            return View(_categoryServise.GetAll(1).OrderBy(c => c.ViewOrder).ToList());
        }

        [HttpPost]
        public IActionResult UpdateViewOrder([FromBody] List<CategoryPositionDTO> orderList)
        {
            _categoryServise.UpdateOrder(orderList);
            return RedirectToAction("Category");
        }

        public async Task<IActionResult> UpdateViewOrderForSubCategories(int cat_id)
        {
            var list = await _categoryServise.GetAllWithSubCategory(1);

            var item = list.FirstOrDefault(c => c.Category_id == cat_id);
            item.SubCategories = item.SubCategories.OrderBy(c => c.ViewOrder).ToList();
            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateViewOrderForSubCategories([FromBody] List<CategoryPositionDTO> orderList)
        {
            _SubcategoryServise.UpdateOrder(orderList);
            return RedirectToAction("Category");
        }

        [HttpPost]
        public IActionResult Category(string SearchTerm)
        {
            ViewData["SearchTerm"] = SearchTerm;
            return View();
        }

        [HttpPost]
        public IActionResult CategoryLeftSortBar(string SearchTerm)
        {
            ViewData["SearchTerm"] = SearchTerm;
            return View();
        }

        public IActionResult AddCategory()
        {
            return View();

        }

        [HttpPost]
        public IActionResult AddCategory(CategoryCMSDTO category)
        {
            _categoryServise.Add(category);
            return RedirectToAction("Category");
            //if (ModelState.IsValid)
            //{
            //    _categoryServise.Add(category);
            //    return RedirectToAction("Category");
            //}
            //else {
            //    throw new J_BadRequestExeption();
            //}

        }
        [HttpPost]
        public IActionResult EditCategory(CategoryCMSDTO category)
        {
            if (ModelState.IsValid)
            {
                _categoryServise.Update(category);
                return RedirectToAction("Category");
            }
            else
            {
                throw new J_BadRequestExeption();
            }
        }

        public IActionResult EditCategory(int id)
        {
            var item = _categoryServise.GetByIdCMS(id);
            return View(item);
        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryServise.Delete(id);
            return RedirectToAction("Category");
        }

        public JsonResult GetCategoryDropList() 
        {
            var list = _categoryServise.GetAll(1);
            var ListJson = list.Select(c => new { c.Category_id, c.Name });
            return Json(ListJson);
        }

        public JsonResult GetSubCategoryDropList(int id)
        {
            var list = _SubcategoryServise.GetByCategory(1,id);
            var ListJson = list.Select(c => new { c.Category_id, c.Name });
            return Json(ListJson);
        }

        public IActionResult AddSubCategory(int catId)
        {
            SubCategoryCMSDTO newSubCat = new SubCategoryCMSDTO
            {
                Category_id = catId,
            };
            return View(newSubCat);
        
        }
        [HttpPost]
        public IActionResult AddSubCategory(SubCategoryCMSDTO SCat)
        {
            _SubcategoryServise.Add(SCat);
            return RedirectToAction("Category");

        }

        public IActionResult EditSubCategory(int id)
        {
            var SubCatToedit = _SubcategoryServise.GetByIdCMS(id);
            return View(SubCatToedit);


        }
        [HttpPost]
        public IActionResult EditSubCategory(SubCategoryCMSDTO SCat)
        {
            _SubcategoryServise.Update(SCat);
            return RedirectToAction("Category");

        }
        public IActionResult DeleteSubCategory(int id)
        {
            _SubcategoryServise.Delete(id);

            return RedirectToAction("Category");
        }

        public IActionResult ConstantCategory() 
        {
            
            return View(_categoryConstantDisplayService.GetDisplaySettings());
        }

        [HttpPost]
        public IActionResult ConstantCategory(CategoryDisplay CategoryDisplay)
        {
            _categoryConstantDisplayService.UpdateDisplaySettings(CategoryDisplay);
            return View(_categoryConstantDisplayService.GetDisplaySettings());
        }


    }
}
