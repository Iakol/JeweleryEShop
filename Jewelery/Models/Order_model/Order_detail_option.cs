using Jewelery.Models.Order_model;

namespace Jewelery.Models.Cart_Model
{
    public class Order_detail_option
    {
        public int Order_detail_option_id { get; set; }

        public int Order_detail_id { get; set; }
        public Order_detail Order_detail { get; set; }
        public int Atribute_Description { get; set; }
        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public decimal PriceAdjustment { get; set; }
    }
}
