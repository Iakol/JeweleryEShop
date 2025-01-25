namespace Jewelery.ViewModels.DTO.DetermineTheSizeEditor
{
    public class DetermineTheSizePageObjectFormDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string TittleUkr { get; set; }
        public string TittleEng { get; set; }
        public List<DetermineTheSizePageObjectParagraphFormDTO> Text { get; set; }
        public List<DetermineTheSizePageObjectPhoroFormDTO> Photos { get; set; }
    }
}
