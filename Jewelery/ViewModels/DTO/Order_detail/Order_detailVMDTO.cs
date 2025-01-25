using Jewelery.Models.Cart_Model;
using Jewelery.ViewModels.DTO.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jewelery.ViewModels.DTO.Order_detail
{
    public class Order_detailVMDTO
    {
        public int Order_detail_id { get; set; }
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public string Product_Name { get; set; }
        public string? Product_Photo_URL { get; set; }
        [NotMapped]
        public ProductDTOVMPage Product { get; set; }
        public List<Order_detail_optionVMDTO> Options { get; set; }
    }
}
