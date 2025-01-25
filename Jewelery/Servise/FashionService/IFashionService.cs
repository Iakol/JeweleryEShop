using Jewelery.Models.Review;
using Jewelery.ViewModels.DTO.Review;

namespace Jewelery.Servise.FashionService
{
    public interface IFashionService
    {
        public void AddFashion(FashionCSMDTO fashion);
       
        public void RemoveFashion(int id);

        public List<Fashion> getAllFashion();
    }
}
