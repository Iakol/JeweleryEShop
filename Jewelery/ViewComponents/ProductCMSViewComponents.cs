using Jewelery.data;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.CMSFilter;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Jewelery.ViewComponents
{
    public class ProductCMSViewComponent : ViewComponent
    {
        private readonly AppDBContext _db;
        private readonly IProductServise _productServise;

        public ProductCMSViewComponent(AppDBContext db, IProductServise productServise)
        {
            _db = db;
            _productServise = productServise;
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductCMSFilterDTO filter) 
        {
            var list = GetSort(filter);
            return View(list);
        }

        public List<ProductCMSDTO> GetSort(ProductCMSFilterDTO filter) 
        {
            var list = _productServise.GetAllCMS().ToList();
            var filerList = new List<ProductCMSDTO>();

            if (!filter.SearchString.IsNullOrEmpty() && list.Count > 0)
            {
                list = list.Where(p => p.Name_UKR.Contains(filter.SearchString) || p.Name_ENG.Contains(filter.SearchString)).ToList();
            }

            if (filter.isExist != null && list.Count > 0) 
            {
                if (filter.isExist == true)
                {
                    list = list.Where(p => p.isExist).ToList();
                }
                else 
                {
                    list = list.Where(p => !p.isExist).ToList();
                }
            }

            if (filter.isDispay != null && list.Count > 0) 
            {
                if (filter.isDispay == true)
                {
                    list = list.Where(p => p.isDisplay).ToList();
                }
                else
                {
                    list = list.Where(p => !p.isDisplay).ToList();
                }
            }

            if (filter.isPromotion != null && list.Count > 0)
            {
                if (filter.isPromotion == true)
                {
                    list = list.Where(p => p.isPromotion).ToList();
                }
                else
                {
                    list = list.Where(p => !p.isPromotion).ToList();
                }
            }

            if (list.Count > 0 && filter.CategoryFiltering)
            {
                foreach (var item in filter.SelectedCategory)
                {
                    if (item.selected)
                    {
                        if (item.SelectedAllSubCategory)
                        {
                            filerList.AddRange(list.Where(p => p.Category_id == item.CategoryId));
                        }
                        else
                        {
                            foreach (var subitem in item.SelectedSubCategory)
                            {
                                if (subitem.Selected)
                                {
                                    filerList.AddRange(list.Where(p => p.SubCategory_id == subitem.SubCategoryId));
                                }
                                else
                                {
                                }
                            }
                        }
                    }
                }
            }
            else 
            {
                filerList = list;
            }

            list = filerList;

            return list;
        }
    }
}
