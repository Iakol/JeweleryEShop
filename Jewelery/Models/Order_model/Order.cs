using Jewelery.Infrastructure.Enums;
using Jewelery.Models.Identity_model;

namespace Jewelery.Models.Order_model
{
    public class Order
    {
        public int Order_id { get; set; }
        public string? User_id { get; set; }
        public string? invoiceId { get; set; }
        public User User { get; set; }
        public decimal Total_price { get; set; }
        public OrderEnum Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Update_at { get; set; }
        public ICollection<Order_detail> Order_Details { get; set; }
        public int Delivery_details_id { get; set; }
        public Delivery_detail Delivery_detail { get; set; }

    }


}
