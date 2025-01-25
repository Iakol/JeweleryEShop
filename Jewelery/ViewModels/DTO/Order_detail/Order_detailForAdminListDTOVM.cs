using System.ComponentModel.DataAnnotations;

namespace Jewelery.ViewModels.DTO.Order_detail
{
    public class Order_detailForAdminListDTOVM
    {
        public int Order_detail_id { get; set; }
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        //public List<Cart_item_option_id> Options { get; set; }

        [MinLength(0)]
        public int Quantity { get; set; }
    }
}
