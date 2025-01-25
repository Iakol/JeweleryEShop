using Jewelery.Models.Product_model;
using System.ComponentModel.DataAnnotations;

namespace Jewelery.Models.Cart_Model
{
    public class Cart_item
    {
        public int Item_id { get; set; }
        public int Cart_id { get; set; }
        public Cart Cart { get; set; }
        public int Product_id { get; set; }
        public Product Product { get; set; }
        public ICollection<Cart_item_option> Options { get; set; }

        
    }
}
