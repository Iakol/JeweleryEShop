namespace Jewelery.ViewModels.DTO.DetermineTheSizeEditor
{
    public class DetermineTheSizePageObjectPhoroFormDTO
    {
        public int ObjectId { get; set; }
        public int placeInOrder { get; set; }
        public IFormFile? imageUKR {  get; set; }
        public IFormFile? imageENG { get; set; }
        
        public bool BothSame { get; set; }
        public string? ChangeUKR { get; set; }
        public string? ChangeENG { get; set; }


    }
}
