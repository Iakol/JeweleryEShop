namespace Jewelery.ViewModels.DTO.Cart_item_option
{
    public class Cart_item_optionDTOVM
    {
        public int Cart_item_option_id { get; set; }
        public int Cart_item_id { get; set; }
        public string Atribute_Description { get; set; }
        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public decimal PriceAdjustment { get; set; }
    }
}
