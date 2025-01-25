using Jewelery.data;
using Jewelery.Servise.CategoryServise;
using Jewelery.ViewModels.DTO.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Jewelery.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly AppDBContext _db;
        private readonly ICategoryServise _categoryServise;
        private readonly CategoryConstantDisplayService _categoryConstantDisplayService;


        public CategoryListViewComponent(AppDBContext db, ICategoryServise categoryServise, CategoryConstantDisplayService categoryConstantDisplayService)
        {
            _db = db;
            _categoryServise = categoryServise;
            _categoryConstantDisplayService = categoryConstantDisplayService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lang) {


            var Config = _categoryConstantDisplayService.GetDisplaySettings();
            ViewData["Sales"] = Config.CategoryDisplay.Sales;
            ViewData["Sertificat"] = Config.CategoryDisplay.Sertificat;
            var item = await GetCategoryVMDTOs(lang);
            return View( item);



        }

        public async Task<List<CategoryVMDTO>> GetCategoryVMDTOs(int lang)
        {
            var list = await _categoryServise.GetAllWithSubCategory(lang);
            var newList = list.OrderBy(c => c.ViewOrder).ToList();
            foreach (var item in newList)
            {
                item.SubCategories = item.SubCategories.OrderBy(sc => sc.ViewOrder).ToList();
            }

            return newList;

        }

    }
}
