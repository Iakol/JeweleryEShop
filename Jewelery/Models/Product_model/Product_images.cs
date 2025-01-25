namespace Jewelery.Models.Product_model
{
    public class Product_images
    {
        public int Image_id { get; set; }
        public int Product_id { get; set; }
        public Product Product { get; set; }
        public string Url { get; set; }
        public string Alt_text { get; set; }
    }
}
