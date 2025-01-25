namespace Jewelery.ViewModels.DTO.DetermineTheSizeEditor
{
    public class DetermineTheSizePageDTO
    {
        public int id { get; set; } = 0;
        public string name { get; set; } = "";
        public string TittleUKR { get; set; } = "";
        public string TittleENG { get; set; } = "";

        public List<DetermineTheSizePageObjectDTO> Page { get; set; } = new List<DetermineTheSizePageObjectDTO>();

    }
}
