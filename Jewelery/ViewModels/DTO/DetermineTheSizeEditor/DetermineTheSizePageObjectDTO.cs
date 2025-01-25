namespace Jewelery.ViewModels.DTO.DetermineTheSizeEditor
{
    public class DetermineTheSizePageObjectDTO
    {
        public int ObjectId { get; set; }
        public int placeInOrder { get; set; }
        public string valueUKR { get; set; } // Photo url or text of paragraph UKR
        public string valueENG { get; set; } // Photo url or text of paragraph ENG

        public bool type {  get; set; } // false is text and true is image
    }
}
