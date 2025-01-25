using Jewelery.Models.Cart_Model;
using Jewelery.ViewModels.DTO.Cart_item;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jewelery.ViewModels.DTO.Cart
{
    public class CartDTOVM
    {
        public int? Cart_id { get; set; }

        public List<Cart_itemDTOVM> cart_ItemDTOVMs { get; set; }

        [NotMapped]
        public decimal TotalPrice
        { get
            {
                decimal totalPrice = 0;

                foreach (var item in cart_ItemDTOVMs)
                {
                    totalPrice = totalPrice + item.TotalPrice;
                }

                return totalPrice;
            }
        }
    }
}
