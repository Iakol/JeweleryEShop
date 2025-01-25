using Jewelery.Servise.CategoryServise;
using Jewelery.ViewModels.DTO.Category;

namespace Jewelery.ViewModels.DTO.CMSFilter
{
    public class ProductCMSFilter
    {
        private readonly ICategoryServise _categoryServise;

        public ProductCMSFilter(ICategoryServise categoryServise) 
        {
            _categoryServise = categoryServise;
            SelectedCategory = Task.Run(async () => await _categoryServise.GetAllWithSubCategory(1))
                .Result
                .Select(c => new ProductCMSFilterCategory
                {
                    CategoryId = c.Category_id,
                    selected = true,
                    SelectedAllSubCategory = true,
                    SelectedSubCategory = c.SubCategories.Select(sc => new ProductCMSFilterSubCategory
                    {
                        SubCategoryId = sc.SubCategory_id,
                        Selected = true
                    }).ToList(),
                }).ToList();
            CategoryFiltering = false;

        }
        public List<ProductCMSFilterCategory> SelectedCategory { get; set; }
        public bool CategoryFiltering { get; set; }
        public bool? isExist { get; set; }
        public bool? isDispay { get; set;}
        public bool? isPromotion { get; set; }

    }

    public class ProductCMSFilterDTO 
    {
        public List<ProductCMSFilterCategory> SelectedCategory { get; set; }
        public bool CategoryFiltering { get; set; }
        public bool? isExist { get; set; }
        public bool? isDispay { get; set; }
        public bool? isPromotion { get; set; }
        public string? SearchString { get; set; }
    }

    public class ProductCMSFilterCategory
    {
        
        public int CategoryId { get; set;}
        public bool selected { get; set; }
        public bool SelectedAllSubCategory { get; set; }
        public List<ProductCMSFilterSubCategory> SelectedSubCategory { get; set; }
    
    }

    public class ProductCMSFilterSubCategory 
    {
        public int SubCategoryId { get; set; }
        public bool Selected { get; set; }
    }
}
