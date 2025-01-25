using Jewelery.Models.Cart_Model;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Cart_item;
using Jewelery.ViewModels.DTO.Cart_item_option;
using Jewelery.ViewModels.DTO.Order_detail;

namespace Jewelery.Servise.OrderDetailService
{
    public interface IOrderDetailService
    {
        public void CreateOrderDetails(CartDTOVM Cart, int OrderId, HttpContext context);
        public void CreateOrderDetail(Cart_itemDTOVM item, int OrderId, HttpContext context);
        public void CreateOrderOptions(List<Cart_item_optionDTOVM> options, int orderDetailId, HttpContext context, Cart_item? cartItem);
        public List<Order_detailVMDTO> GetOrderDetailByOrderId(int OrderId, int lang);



    }
}
