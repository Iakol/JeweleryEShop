using Jewelery.Models.Order_model;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Order;
using Jewelery.ViewModels.DTO.Order_detail;

namespace Jewelery.Servise.OrderService
{
    public interface IOrderService
    {
        public int PreCreateOrder(CartDTOVM Cart, Delivery_detail deliveryDetail, HttpContext context, string? UserId);
        public void CreateDeliveryDetail(Delivery_detail deliveryDetail, int OrderId);
        public List<OrderVMDTO> GetOrderListByUserID(string UserID, int lang);
        public List<OrderVMDTO> GetAllOrderList(int lang);// Це ДТО всех Заказов
        public OrderVMDTO GetCurrentOrder(int lang, int OrderID);// Це для конкретного замовлення

        public void UpdateOrderStatusByPaimentApp(int status, string invoiceId);// Упдейт статусов
        public void UpdateOrderStatus(int status, int orderId);


        public void SetInvoiceId(int OrderId, string invoiceId);

    }
}
