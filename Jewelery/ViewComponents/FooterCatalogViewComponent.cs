using Jewelery.Servise.CategoryServise;
using Jewelery.ViewModels.DTO.Category;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.ViewComponents
{
    public class FooterCatalogViewComponent : ViewComponent
    {
        private readonly ICategoryServise _categoryServise;

        public FooterCatalogViewComponent(ICategoryServise categoryServise)
        {
            _categoryServise = categoryServise;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lang)
        {
            List<CategoryVMDTO> categoryList = await _categoryServise.GetAllWithSubCategory(lang);
            categoryList = categoryList.OrderBy(c => c.ViewOrder).ToList();
            return View(categoryList);
        }




    }
}
