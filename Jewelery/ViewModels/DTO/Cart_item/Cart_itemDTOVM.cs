using Jewelery.ViewModels.DTO.Option;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Cart_item_option;
using Jewelery.ViewModels.DTO.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jewelery.ViewModels.DTO.Cart_item
{
    public class Cart_itemDTOVM
    {
        public int Item_id { get; set; }
        public ProductDTOVMPage Product { get; set; }
        public List<Cart_item_optionDTOVM> Options { get; set; }



        [NotMapped]
        public decimal TotalPrice { get 
            {
                decimal AdjustPrice = 0;

                foreach (var item in Options)
                {
                    AdjustPrice = AdjustPrice + item.PriceAdjustment;

                }
                decimal Price = Product.Price + AdjustPrice;
                return Price;

            } }

    }
}
