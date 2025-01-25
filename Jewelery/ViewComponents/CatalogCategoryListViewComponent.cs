using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.AspNetCore.Mvc;


namespace Jewelery.ViewComponents
{
    public class CatalogCategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryServise _categoryServise;
        private readonly CategoryConstantDisplayService _categoryConstantDisplayService;

        public CatalogCategoryListViewComponent(ICategoryServise categoryServise, CategoryConstantDisplayService categoryConstantDisplayService)
        {
            _categoryServise = categoryServise;
            _categoryConstantDisplayService = categoryConstantDisplayService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? cat_id, int lang)
        {

            var Config = _categoryConstantDisplayService.GetDisplaySettings();
            ViewData["Sales"] = Config.CategoryDisplay.Sales;
            ViewData["Sertificat"] = Config.CategoryDisplay.Sertificat;

            if (cat_id != null && cat_id != 0)
            {
                return View(await getSort(lang,cat_id));

            }
            else 
            {
                return View(await getSort(lang));

            }


        }

        public async Task<List<CategoryVMDTO>> getSort(int lang,int? cat_id = null) 
        {
            if (cat_id != null && cat_id != 0)
            {
                var list = await _categoryServise.GetAllWithSubCategory(lang);

                list = list.Where(c => c.Category_id == cat_id).ToList();

                return list;

            }
            else 
            {
                return await _categoryServise.GetAllWithSubCategory(lang);
            }
        }
    }
}
