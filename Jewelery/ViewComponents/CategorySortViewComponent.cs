using Jewelery.data;
using Jewelery.Servise.CategoryServise;
using Jewelery.ViewModels.DTO.Category;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.ViewComponents
{
    public class CategorySortViewComponent : ViewComponent
    {
        private readonly AppDBContext _db;
        private readonly ICategoryServise _categoryServise;

        public CategorySortViewComponent(AppDBContext db, ICategoryServise categoryServise) {
            _db = db;
            _categoryServise = categoryServise;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lang)
        {

            var item = await GetSort(lang);
            return View(item);
        }

        public async Task<List<CategoryVMDTO>> GetSort(int lang)
        {
            var list = await _categoryServise.GetAllWithSubCategory(lang);
            var newList = list.OrderBy(c => c.ViewOrder).ToList();

            foreach (var item in newList)
            {
                item.SubCategories = item.SubCategories.OrderBy(sc => sc.ViewOrder).ToList();
            }
            return list;

        }

    }
}
