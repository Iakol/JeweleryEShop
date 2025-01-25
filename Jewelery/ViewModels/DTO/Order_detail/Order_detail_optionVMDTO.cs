namespace Jewelery.ViewModels.DTO.Order_detail
{
    public class Order_detail_optionVMDTO
    {
        public int Order_detail_option_id { get; set; }
        public int Order_detail_id { get; set; }
        public string Atribute_Description { get; set; }
        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public decimal PriceAdjustment { get; set; }
    }
}
