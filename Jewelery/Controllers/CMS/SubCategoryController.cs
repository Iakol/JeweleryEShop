using Jewelery.data;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.VMCMS.VMCategory.VMSubCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jewelery.Controllers.CMS
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SubCategoryController : Controller
    {
        private readonly AppDBContext _db;

        public SubCategoryController(AppDBContext db)
        {
            _db = db;
        }
        public IActionResult SubCategoryCMS(int id)
        {
            VMSubCategoryCMS obj = new VMSubCategoryCMS
            {
                Category = _db.Categories.Find(id),
                SubCategories = _db.SubCategories.Where(sc => sc.Category_id == id).ToList()
            };
            return View(obj);
        }

        public IActionResult UpsertSubCategoryCMS(int? id, int category_id)
        {
            SubCategory UpsertSubCategory = new SubCategory();
            UpsertSubCategory.Category_id = category_id;

            if (id == null || id == 0)
            {
                return View(UpsertSubCategory);
            }
            else if (id != null || id != 0)
            {
                UpsertSubCategory = _db.SubCategories.Find(id);
            }
            else
            {
                return NotFound();
            }
            return View(UpsertSubCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertSubCategoryCMS(SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                if (subcategory.SubCategory_id == 0)
                {
                    _db.SubCategories.Add(subcategory);
                }
                else
                {
                    _db.SubCategories.Update(subcategory);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(SubCategoryCMS), new {id = subcategory.Category_id });
            }
            return View(subcategory);
        }

        public IActionResult DeleteSubCategoryCMS(int id, int category_id) 
        {
            SubCategory SubCategoryTodelete = _db.SubCategories.Find(id);
            _db.SubCategories.Remove(SubCategoryTodelete);
            _db.SaveChanges();
            return RedirectToAction(nameof(SubCategoryCMS), new { id = category_id });
        }

       

    }
}
