using Jewelery.data;
using Jewelery.Servise.CategoryServise;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.SubCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Jewelery.ViewComponents
{
    public class CategoryCMSListViewComponent : ViewComponent
    {
        private readonly AppDBContext _db;
        private readonly ICategoryServise _categoryServise;
        private readonly ISubCategoryServise _subcategoryServise;


        public CategoryCMSListViewComponent(AppDBContext db, ICategoryServise categoryServise, ISubCategoryServise subcategoryServise)
        {
            _db = db;
            _categoryServise = categoryServise;
            _subcategoryServise = subcategoryServise;
        }

        public async Task<IViewComponentResult> InvokeAsync(string Search)
        {

            return View(getSort(Search));

        }

        public List<CategoryListCMS> getSort(string Search) {
            var CList = _db.Categories.Select(c => c.Category_id);
            List<CategoryListCMS> RList = new List<CategoryListCMS>();

            foreach (var item in CList) {
                var categoryCSMDTO = _categoryServise.GetByIdCMS(item);
                var SubCategoryList = _db.SubCategories.Where(c => c.Category_id == item).Select(c => c.SubCategory_id);
                List<SubCategoryCMSDTO> SubCategories = new List<SubCategoryCMSDTO>();
                foreach (int sci in SubCategoryList) {
                    SubCategories.Add(_subcategoryServise.GetByIdCMS(sci));
                }

                CategoryListCMS category = new CategoryListCMS
                {
                    Category_id = categoryCSMDTO.Category_id,
                    Name_UKR = categoryCSMDTO.Name_UKR,
                    Name_ENG = categoryCSMDTO.Name_ENG,
                    Description_UKR = categoryCSMDTO.Description_UKR,
                    Description_ENG = categoryCSMDTO.Description_ENG,
                    ViewOrder = categoryCSMDTO.ViewOrder,
                    Image = categoryCSMDTO.Image,
                    SubCategories = SubCategories

                };
                RList.Add(category);

            }

            if (!Search.IsNullOrEmpty()) {
                RList = RList.Where(c => c.Name_UKR.Contains(Search) ||
                c.Name_ENG.Contains(Search) || c.SubCategories.Any(sc=>sc.Name_UKR.Contains(Search) || sc.Name_ENG.Contains(Search))).ToList();
                return RList;
            }
            return RList;
        
        }
    }
}
