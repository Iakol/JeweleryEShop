using Jewelery.Models.Order_model;
using Jewelery.ViewModels.DTO.NovaPost;

namespace Jewelery.Servise.NovaPostServise
{
    public interface INovaPostServise
    {
        public  Task<NovaPostCityResponce> GetCityListByName(string CityName);
        public Task<NovaPostPostResponce> GetPostListByCityRef(string SityRef);

        public Task<CreateDeliveryDocumentResponce> CreateDeliveryDocument(Order order);

        public void GetOrderStatus();

    }
}
