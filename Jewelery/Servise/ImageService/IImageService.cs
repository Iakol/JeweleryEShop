using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.DetermineTheSizeEditor;
using Jewelery.ViewModels.DTO.Product;
using Jewelery.ViewModels.DTO.Product_images;
using Jewelery.ViewModels.DTO.Review;
using Jewelery.ViewModels.DTO.SubCategory;

namespace Jewelery.Servise.ImageService
{
    public interface IImageService
    {
        public string AddCategotyImage(CategoryCMSDTO img);
        public string AddSubCategotyImage(SubCategoryCMSDTO img);

        public ProductCMSDTO AddImageToProduct(ProductCMSDTO img);
        public void DeleteImageFromProduct(List<Product_images> img);

        public void DeleteImage(string img);

        public string AddFashionImage(FashionCSMDTO img);

        public string CreateDetermineSizeObjectPhotoImage(IFormFile Photo);
        public void DeleteDetermineSizeObjectPhotoImage(string url);

        public string DetermineSizeCopyImage(string url);


    }
}
