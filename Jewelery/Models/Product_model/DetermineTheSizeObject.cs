namespace Jewelery.Models.Product_model
{
    public class DetermineTheSizeObject
    {
        public int DetermineTheSizeObject_Id {  get; set; }
        public int DetermineTheSize_Id { get; set; }
        public DetermineTheSize DetermineTheSize { get; set; }
        public int Value { get; set; } //Text or Url
        public bool Type {  get; set; } // false is text and true is image
        public int placeInOrder { get; set; }

    }
}
