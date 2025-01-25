using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using System.ComponentModel.DataAnnotations;

namespace Jewelery.Models.Order_model
{
    public class Order_detail
    {
        public int Order_detail_id { get; set; }
        public int Order_id { get; set; }
        public Order Order { get; set; }
        public int Product_id { get; set; }
        public int Product_Name { get; set; }
        public string? Product_Photo_URL { get; set; }
        public Product Product { get; set; }
        public ICollection<Order_detail_option> Options { get; set; }


    }
}
