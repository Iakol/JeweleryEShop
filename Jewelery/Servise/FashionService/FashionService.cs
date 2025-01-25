using Jewelery.data;
using Jewelery.Models.Review;
using Jewelery.Servise.ImageService;
using Jewelery.ViewModels.DTO.Review;

namespace Jewelery.Servise.FashionService
{
    public class FashionService : IFashionService
    {
        private readonly IImageService _imageService;
        private readonly AppDBContext _db;

        public FashionService(IImageService imageService, AppDBContext db)
        {
            _imageService = imageService;
            _db = db;
        }

        public void AddFashion(FashionCSMDTO fashion)
        {
            Fashion fashionToadd = new Fashion
            {
                Id = fashion.Id,
                Image = _imageService.AddFashionImage(fashion)
            };

            _db.Fashions.Add(fashionToadd);
            _db.SaveChanges();

        }

        public List<Fashion> getAllFashion()
        {
            return _db.Fashions.ToList();
        }

        public void RemoveFashion(int id)
        {
            Fashion fas = _db.Fashions.FirstOrDefault(f => f.Id == id);

            _imageService.DeleteImage(fas.Image);
            _db.Fashions.Remove(fas);
            _db.SaveChanges();

        }
    }
}
