namespace Jewelery.Models.Product_model
{
    public class Atribute
    {
        public int Atribute_id { get; set; }
        public int Atribute_name { get; set; }
        public int Product_id { get; set; }
        public Product Product { get; set; }
        public ICollection<Option> Options { get; set; }
        public string? Unit { get; set; }

        public int? DetermineTheSize_Id { get; set; }
        public DetermineTheSize DetermineTheSize { get; set; }
    }
}
