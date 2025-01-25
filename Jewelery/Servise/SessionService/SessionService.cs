using Jewelery.Models.Cart_Model;
using Jewelery.Servise.Cart_itemServise;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text.Json;

namespace Jewelery.Servise.SessionService
{
    public class SessionService : ISessionService
    {
        private readonly ICart_itemServise _cart_itemServise;
        private const string CartStringKey = "Cart";
        private const string CartItemStringKey = "CartItem";


        public SessionService(ICart_itemServise cart_itemServise)
        {
            _cart_itemServise = cart_itemServise;
        }

        public void AddItemToCart(ProductDTOVMPage ProductTOCart, HttpContext context) 
        {
            if (string.IsNullOrEmpty(context.Session.GetString(CartStringKey))) 
            {
                List<Cart_item> CreateCart = new List<Cart_item>();
                context.Session.SetString(CartStringKey, JsonSerializer.Serialize(CreateCart));
            }

            var cart = JsonSerializer.Deserialize<List<Cart_item>>(context.Session.GetString(CartStringKey));

            int MaxId = 0;

            if(cart.Count > 0) 
            {
                MaxId = cart.Max(i => i.Item_id) + 1;
            }

            cart.Add(_cart_itemServise.CreateCartItemForSession(ProductTOCart, context, MaxId));

            context.Session.SetString(CartStringKey, JsonSerializer.Serialize(cart));
        }

        public void DeleteItemFromCart(int ItemId, HttpContext context) 
        {
            var cart = JsonSerializer.Deserialize<List<Cart_item>>(context.Session.GetString(CartStringKey));
            _cart_itemServise.DeleteCartItemFromSession(ItemId, context);
        }

        public CartDTOVM GetCartByUserDTOVM(int lang, HttpContext context)
        {
            return new CartDTOVM
            {
                Cart_id = 0,
                cart_ItemDTOVMs = _cart_itemServise.GetCartItemByCartInSession(lang, context)
            };
        }



    }
}
