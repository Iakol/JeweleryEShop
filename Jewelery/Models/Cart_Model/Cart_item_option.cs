namespace Jewelery.Models.Cart_Model
{
    public class Cart_item_option
    {
        public int Cart_item_option_id { get; set; }

        public int Cart_item_id { get; set; }
        public Cart_item cart_Item { get; set; }
        public int Atribute_Description { get; set; }
        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public decimal PriceAdjustment { get; set; }
    }
}
