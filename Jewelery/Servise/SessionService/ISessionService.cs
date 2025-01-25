using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Product;

namespace Jewelery.Servise.SessionService
{
    public interface ISessionService
    {
        public void AddItemToCart(ProductDTOVMPage ProductTOCart, HttpContext context);
        public void DeleteItemFromCart(int ItemId, HttpContext context);
        public CartDTOVM GetCartByUserDTOVM(int lang, HttpContext context);
    }
}
