using Jewelery.Infrastructure.Enums;
using Jewelery.Models.Order_model;
using Jewelery.ViewModels.DTO.Order_detail;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jewelery.ViewModels.DTO.Order
{
    public class OrderVMDTO
    {
        public int Order_id { get; set; }
        public string? User_id { get; set; }
        public Jewelery.Models.Identity_model.User? User { get; set; }
        public decimal Total_price { get; set; }
        public OrderEnum Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Update_at { get; set; }
        public List<Order_detailVMDTO> Order_Details { get; set; }
        [NotMapped]
        public Delivery_detailVMDTO Delivery_detail { get; set; }
    }
}
