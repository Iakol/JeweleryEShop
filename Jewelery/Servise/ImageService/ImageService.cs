using Jewelery.data;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.DetermineTheSizeEditor;
using Jewelery.ViewModels.DTO.Product;
using Jewelery.ViewModels.DTO.Product_images;
using Jewelery.ViewModels.DTO.Review;
using Jewelery.ViewModels.DTO.SubCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IO;

namespace Jewelery.Servise.ImageService
{
    public class ImageService : IImageService
    {
        private readonly AppDBContext _db;
        public ImageService(AppDBContext db)
        {
            _db = db;


        }
        public string AddCategotyImage(CategoryCMSDTO img)
        {
            string fileName = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
            string FileExtension = Path.GetExtension(img.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + FileExtension;
            string imagePatch = Path.Combine("wwwroot/image/Category", fileName);
            using (var fileStream = new FileStream(imagePatch, FileMode.Create))
            {
                img.ImageFile.CopyTo(fileStream);
            }

            DeleteImage(img.Image);

            return "/image/Category/" + fileName;
        }

        public string AddSubCategotyImage(SubCategoryCMSDTO img)
        {
            string fileName = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
            string FileExtension = Path.GetExtension(img.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + FileExtension;
            string imagePatch = Path.Combine("wwwroot/image/SubCategory", fileName);
            using (var fileStream = new FileStream(imagePatch, FileMode.Create))
            {
                img.ImageFile.CopyTo(fileStream);
            }

            DeleteImage(img.Image);

            return "/image/SubCategory/" + fileName;
        }

        public ProductCMSDTO AddImageToProduct(ProductCMSDTO product)
        {
            List<int> OldImages = _db.Product_images.Where(i => i.Product_id == product.Product_id).Select(i => i.Image_id).ToList();

            foreach (int item in OldImages)
            {
                if (product.Images == null||!product.Images.Any(i => i.Image_id == item))
                {
                    DeleteImage(_db.Product_images.FirstOrDefault(i => i.Image_id == item).Url.TrimStart('/'));
                    //var ImageToDelete = product.Images.FirstOrDefault(i => i.Image_id == item);
                    //product.Images.Remove(ImageToDelete);
                    _db.Product_images.Remove(_db.Product_images.Find(item));

                }
                
            }

            _db.SaveChanges();

            if(product.Images != null && product.Images.Any(i => i.Image_id == 0)) { 

            List<Product_imagesDTO> ImageToAdd = product.Images.Where(i => i.Image_id == 0).ToList();

            foreach (var item in ImageToAdd)
                {
                    var FileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                    var FileExtension = Path.GetExtension(item.ImageFile.FileName);
                    FileName = FileName + DateTime.Now.ToString("yymmssfff") + FileExtension;
                    string ImagePath = Path.Combine("wwwroot/image/Product", FileName);
                    using (var fileStream = new FileStream(ImagePath, FileMode.Create))
                    {
                        item.ImageFile.CopyTo(fileStream);
                        item.Url = "/image/Product/" + FileName;
                    }
                }
            }

            return product;

        }

        public void DeleteImageFromProduct(List<Product_images> img)
        {
            
            foreach (var item in img)
            {
                DeleteImage(item.Url);
                if (_db.Product_images.Any(i => i.Image_id == item.Image_id)) 
                {
                    _db.Product_images.Remove(item);
                }
             }
           

            if (_db.ChangeTracker.HasChanges()) 
            {
                _db.SaveChanges();
            }


        }

        public string AddFashionImage(FashionCSMDTO img) 
        {
            string fileName = Path.GetFileNameWithoutExtension(img.Image.FileName);
            string fileExtension = Path.GetExtension(img.Image.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtension;
            string ImagePath = Path.Combine("wwwroot/image/Fashion", fileName);

            using (var FileStream = new FileStream(ImagePath, FileMode.Create)) 
            {
                img.Image.CopyTo(FileStream);
            }

            return "/image/Fashion/" + fileName;
        }


        public void DeleteImage(string img)
        {
            if (!img.IsNullOrEmpty())
            {
                var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", img.TrimStart('/'));
                if (System.IO.File.Exists(OldPath))
                {

                    System.IO.File.Delete(OldPath);
                }

            }


        }

        public string CreateDetermineSizeObjectPhotoImage(IFormFile Photo)
        {
            string fileName = Path.GetFileNameWithoutExtension(Photo.FileName);
            string fileExtension = Path.GetExtension(Photo.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString() + fileExtension;
            string ImagePath = Path.Combine("wwwroot/image/DetermineTheSize", fileName);

            using (var FileStream = new FileStream(ImagePath, FileMode.Create))
            {
                Photo.CopyTo(FileStream);
            }

            return "/image/DetermineTheSize/" + fileName;
        }

        public void DeleteDetermineSizeObjectPhotoImage(string url)
        {
            DeleteImage(url);
        }

        public string DetermineSizeCopyImage(string url) 
        {
            string file = Path.GetFileName(url);
            string fileName = Path.GetFileNameWithoutExtension(file);
            string fileExtension = Path.GetExtension(file);
            string NewfileName = fileName + Guid.NewGuid() + fileExtension;
            string NewFileUrl = Path.Combine(Path.GetDirectoryName(url).TrimStart('/'), NewfileName);

            string FromUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url.TrimStart('/'));
            string ToUrl = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", NewFileUrl.TrimStart('\\'));

            File.Copy(FromUrl, ToUrl, true);

            return NewFileUrl;

        }

    }
}
