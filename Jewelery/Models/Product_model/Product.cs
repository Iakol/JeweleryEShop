namespace Jewelery.Models.Product_model
{
    public class Product
    {
        public int Product_id { get; set; }
        public int Name { get; set; }
        public int Description { get; set; }
        public decimal Price { get; set; }
        public int Category_id { get; set; }
        public int Articul { get; set; }
        public Category Category { get; set; }
        public int? SubCategory_id { get; set; }
        public SubCategory SubCategory { get; set; }
        public ICollection<Product_images> Images { get; set; }
        public ICollection<Atribute> Attributes { get; set; }
        public bool isExist { get; set; }
        public bool isDisplay { get; set; }
        public bool isPromotion { get; set; }
        public decimal? Promotion_Price { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
