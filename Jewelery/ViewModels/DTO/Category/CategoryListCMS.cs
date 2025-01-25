using Jewelery.ViewModels.DTO.SubCategory;

namespace Jewelery.ViewModels.DTO.Category
{
    public class CategoryListCMS
    {
        public int Category_id { get; set; }
        public string Name_UKR { get; set; }
        public string Name_ENG { get; set; }

        public string Description_UKR { get; set; }
        public string Description_ENG { get; set; }
        public int ViewOrder { get; set; }


        public string Image { get; set; }
        public List<SubCategoryCMSDTO> SubCategories { get; set; }

    }
}
