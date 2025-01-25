using Jewelery.Models.Product_model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jewelery.ViewModels.DTO.Category
{
    public class CategoryCMSDTO
    {
        public int Category_id { get; set; }
        public string Name_UKR { get; set; }
        public string Name_ENG { get; set; }

        public string Description_UKR { get; set; }
        public string Description_ENG { get; set; }
        public int ViewOrder { get; set; }


        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }


    }
}
