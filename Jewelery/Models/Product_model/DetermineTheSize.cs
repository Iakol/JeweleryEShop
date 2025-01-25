namespace Jewelery.Models.Product_model
{
    public class DetermineTheSize
    {
        public int DetermineTheSize_Id { get; set; }
        public string Name { get; set; } 
        public int Tittle { get; set; }
        public ICollection<DetermineTheSizeObject> determineTheSizeObjects { get; set; }
    }
}
