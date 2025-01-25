using Jewelery.Models.Cart_Model;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Product;

namespace Jewelery.Servise.CartServise
{
    public interface ICartServise
    {
        public Cart GetCartByUser(string UserId);
        public CartDTOVM GetCartByUserDTOVM(string UserId, int lang); // Якась ДТО З карт итемами
        public void CreateCartByUser(string UserId);

        public void AddItemToCart(ProductDTOVMPage ProductTOCart, string UserId);
        public void DeleteItemFromCart(int ItemId);

        public void RemoveCartIfCartisEmpty(int Cart_id);

        
       
    }
}
