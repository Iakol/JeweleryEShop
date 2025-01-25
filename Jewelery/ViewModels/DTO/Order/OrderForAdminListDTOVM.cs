using Jewelery.Infrastructure.Enums;
using Jewelery.Models.Order_model;
using Jewelery.ViewModels.DTO.Order_detail;

namespace Jewelery.ViewModels.DTO.Order
{
    public class OrderForAdminListDTOVM
    {
        public int Order_id { get; set; }
        public string User_id { get; set; }
        public decimal Total_price { get; set; }
        public OrderEnum Status { get; set; }
        public List<Order_detailVMDTO> OrderItems { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Update_at { get; set; }

    }
}
