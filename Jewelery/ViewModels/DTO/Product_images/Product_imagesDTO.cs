using Jewelery.Models.Product_model;

namespace Jewelery.ViewModels.DTO.Product_images
{
    public class Product_imagesDTO
    {
        public int Image_id { get; set; }
        public int Product_id { get; set; }
        public string Url { get; set; }
        public IFormFile ImageFile { get; set; }

        public string Alt_text { get; set; }
    }
}
