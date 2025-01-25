namespace Jewelery.Models.Product_model
{
    public class Option
    {
        public int Option_id { get; set; }
        public int Atribute_id { get; set; }
        public Atribute Atribute { get; set; }
        public decimal Size { get; set; }
        public decimal PriceAdjustment { get; set; }
    }
}
