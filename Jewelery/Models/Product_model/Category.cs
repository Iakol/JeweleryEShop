namespace Jewelery.Models.Product_model
{
    public class Category
    {
        public int Category_id { get; set; }
        public int Name { get; set; }
        public int Description { get; set; }
        public string Image {get; set; }
        public bool isDisplay { get; set; }
        public int ViewOrder { get; set; }
        public List<Product> Products { get; set; }
        public List<SubCategory> SubCategories { get; set; }


    }
}
