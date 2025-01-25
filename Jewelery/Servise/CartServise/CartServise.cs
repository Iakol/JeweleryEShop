using Jewelery.data;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Cart_Model;
using Jewelery.Servise.Cart_itemServise;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Cart_item;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.Servise.CartServise
{
    public class CartServise : ICartServise
    {
        private readonly AppDBContext _db;
        private readonly ICart_itemServise _ItemServise;

        public CartServise(AppDBContext db, ICart_itemServise ItemServise)
        {
            _db = db;
            _ItemServise = ItemServise;
        }

        public void AddItemToCart(ProductDTOVMPage ProductTOCart, string UserId)
        {
            var UserCart = GetCartByUser(UserId);

            if (UserCart == null)
            {
                CreateCartByUser(UserId);
                AddItemToCart(ProductTOCart, UserId);
            }
            else if (UserCart != null)
            {
                _ItemServise.CreateCartItem(ProductTOCart, UserCart);
            }
            else {
                throw new J_ServiceTemporarilyUnavailableExeption();
            }

        }

        public void AddItemsToCartFromSessionCart( string UserId, HttpContext context)
        {
            var UserCart = GetCartByUser(UserId);

            if (UserCart == null)
            {
                CreateCartByUser(UserId);
                AddItemsToCartFromSessionCart( UserId, context);
            }
            else if (UserCart != null)
            {
                _ItemServise.CreateCartItemFromSessionCart(UserCart, context);
            }
            else
            {
                throw new J_ServiceTemporarilyUnavailableExeption();
            }

        }

        public void CreateCartByUser(string UserId)
        {
            _db.Carts.Add(new Cart
            {
                User_id = UserId,
                Created_at = DateTime.Now,
                Update_at = DateTime.Now
            });
            _db.SaveChanges();
        }

        public void DeleteItemFromCart(int ItemId)
        {
            int Cart_id = _db.Carts_items.FirstOrDefault(ci => ci.Item_id == ItemId).Cart_id;
            _ItemServise.DeleteCartItem(ItemId);
            RemoveCartIfCartisEmpty(Cart_id);


        }

        public Cart GetCartByUser(string UserId)
        {
            return _db.Carts.FirstOrDefault(c => c.User_id == UserId);
        }

        public CartDTOVM GetCartByUserDTOVM(string UserId, int lang)
        {
            return new CartDTOVM {
                Cart_id = _db.Carts.FirstOrDefault(c => c.User_id == UserId).Cart_id,
                cart_ItemDTOVMs = _ItemServise.GetCartItemByCartID(_db.Carts.FirstOrDefault(c => c.User_id == UserId).Cart_id, lang)
            }; 
        }

        public void RemoveCartIfCartisEmpty(int Cart_id)
        {
            if (
                (_db.Carts_items.Where(ci => ci.Cart_id == Cart_id).ToList()).Count() == 0
               )
            {
                _db.Carts.Remove(_db.Carts.FirstOrDefault(c => c.Cart_id == Cart_id));
            }
            else;
        }
    }
}
