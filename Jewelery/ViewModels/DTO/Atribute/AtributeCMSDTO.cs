
namespace Jewelery.ViewModels.DTO.Atribute

{
    using Jewelery.Models.Product_model;
    public class AtributeCMSDTO
    {
        public int Atribute_id { get; set; }
        public string Atribute_name_UKR { get; set; }
        public string Atribute_name_ENG { get; set; }

        public int Product_id { get; set; }
        public List<Option> Options { get; set; }
        public string? Unit { get; set; }

        public int? DetermineTheSize_Id { get; set; }
    }
}
