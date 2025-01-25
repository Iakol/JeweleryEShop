using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.DetermineTheSizeEditor;
using Jewelery.ViewModels.DTO.Option;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jewelery.ViewModels.DTO.Atribute
{
    public class AtributeDTO
    {
        public int Atribute_id { get; set; }
        public string Atribute_name { get; set; }
        public int Product_id { get; set; }
        public string? Unit { get; set; }

        public List<Jewelery.Models.Product_model.Option> Options { get; set; }

        public int? DetermineTheSize_Id { get; set; }

        [NotMapped]
        public DetermineTheSizePageDTO? DetermineTheSize_Page { get; set;}
    }
}
