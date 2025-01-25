using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.SubCategory;

namespace Jewelery.ViewModels.DTO.Category
{
    public class CategoryVMDTO
    {
        public int Category_id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public int ViewOrder { get; set; }

        public List<SubCategoryVMDTO> SubCategories { get; set; }

    }
}
