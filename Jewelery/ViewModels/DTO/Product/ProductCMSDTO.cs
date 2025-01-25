using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Product_images;

namespace Jewelery.ViewModels.DTO.Product
{
    public class ProductCMSDTO
    {
        public int Product_id { get; set; }
        public string Name_UKR { get; set; }
        public string Name_ENG { get; set; }

        public string Description_UKR { get; set; }
        public string Description_ENG { get; set; }

        public decimal Price { get; set; }
        public int Category_id { get; set; }
        public int Articul { get; set; }
        public int? SubCategory_id { get; set; }
        public List<Product_imagesDTO> Images { get; set; }
        public List<AtributeCMSDTO> Attributes { get; set; }
        public bool isExist { get; set; }
        public bool isDisplay { get; set; }
        public bool isPromotion { get; set; }
        public decimal? Promotion_Price { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
