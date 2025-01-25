using Jewelery.data;
using Jewelery.Infrastructure.Enums;
using Jewelery.Models.Identity_model;
using Jewelery.Models.Order_model;
using Jewelery.Servise.CartServise;
using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.IdentityService;
using Jewelery.Servise.OrderDetailService;
using Jewelery.Servise.SessionService;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Order;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Web.Helpers;

namespace Jewelery.Servise.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDBContext _db;
        private readonly IOrderDetailService _orderDetailService;

        public OrderService(AppDBContext db, IOrderDetailService orderDetailService )
        {
            _db = db;
            _orderDetailService = orderDetailService;
        }

        public int PreCreateOrder(CartDTOVM Cart,  Delivery_detail deliveryDetail,HttpContext context, string? UserId )
        {
            SqlParameter User_id = new SqlParameter("@User_id", DBNull.Value);
            if (!UserId.IsNullOrEmpty())
            {
                User_id = new SqlParameter("@User_id", UserId);
            }
            SqlParameter invoiceId = new SqlParameter("@invoiceId", DBNull.Value);
            SqlParameter Total_price = new SqlParameter("@Total_price", Cart.TotalPrice);
            SqlParameter Order_id = new SqlParameter()
            {
                ParameterName = "@Order_id",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int
            };

            _db.Database.ExecuteSqlRaw("EXECUTE PreCreateOrder @User_id,invoiceId, @Total_price, @Order_id OUTPUT",
                User_id, invoiceId, Total_price, Order_id);

            CreateDeliveryDetail(deliveryDetail, (int)Order_id.Value);

            _orderDetailService.CreateOrderDetails(Cart, (int)Order_id.Value, context);

            return (int)Order_id.Value;
        }

        public void CreateDeliveryDetail(Delivery_detail deliveryDetail , int OrderId) 
        {
            SqlParameter Order_id = new SqlParameter("@Order_id", OrderId);
            SqlParameter Delivery_Name = new SqlParameter("@Delivery_Name", deliveryDetail.Delivery_Name);
            SqlParameter Delivery_Second_Name = new SqlParameter("@Delivery_Second_Name", deliveryDetail.Delivery_Second_Name);
            SqlParameter Delivery_Father_Name = new SqlParameter("@Delivery_Father_Name", DBNull.Value);
            if (!deliveryDetail.Delivery_Father_Name.IsNullOrEmpty()) 
            {
                Delivery_Father_Name = new SqlParameter("@Delivery_Father_Name", deliveryDetail.Delivery_Father_Name);
            }
            SqlParameter Email = new SqlParameter("@Email", deliveryDetail.Email);
            SqlParameter Phone_number = new SqlParameter("@Phone_number", deliveryDetail.Phone_number);
            SqlParameter contactWithMe = new SqlParameter("@contactWithMe", deliveryDetail.contactWithMe);
            SqlParameter noteForOrder = new SqlParameter("@noteForOrder", DBNull.Value);
            if (!deliveryDetail.noteForOrder.IsNullOrEmpty()) 
            {
                noteForOrder = new SqlParameter("@noteForOrder", deliveryDetail.noteForOrder);
            }            
            SqlParameter StoreOrDoorDelivery = new SqlParameter("@StoreOrDoorDelivery", deliveryDetail.StoreOrDoorDelivery);
            SqlParameter RecipientWarehouseIndex = new SqlParameter("@RecipientWarehouseIndex", DBNull.Value);
            if (!deliveryDetail.RecipientWarehouseIndex.IsNullOrEmpty()) 
            {
                RecipientWarehouseIndex = new SqlParameter("@RecipientWarehouseIndex", deliveryDetail.RecipientWarehouseIndex);
            }


            _db.Database.ExecuteSqlRaw("EXECUTE CreateDeliveryDetail @Order_id,@Delivery_Name,@Delivery_Second_Name,@Delivery_Father_Name,@Email,@Phone_number,@contactWithMe,@noteForOrder,@StoreOrDoorDelivery,@RecipientWarehouseIndex",
                Order_id, Delivery_Name, Delivery_Second_Name, Delivery_Father_Name, Email, Phone_number, contactWithMe, noteForOrder, StoreOrDoorDelivery, RecipientWarehouseIndex);


        }



        public List<OrderVMDTO> GetAllOrderList(int lang)// Це ДТО всіх Заказов
        {
            List < OrderVMDTO > list = _db.Orders.Include(o => o.User).Select(o => new OrderVMDTO 
            {
                Order_id = o.Order_id,
                User_id = o.User_id,
                User = o.User,
                Total_price = o.Total_price,
                Status = o.Status,
                Created_at = o.Created_at,
                Update_at = o.Update_at,
                Order_Details = new List<ViewModels.DTO.Order_detail.Order_detailVMDTO>(),
                Delivery_detail = new Delivery_detailVMDTO()

            } ).ToList();
            foreach (var item in list) 
            {
                item.Delivery_detail = _db.Delivery_details.Select(d => new Delivery_detailVMDTO {
                    Delivery_detail_id = d.Delivery_detail_id,
                    Order_id = d.Order_id,
                    Delivery_Name = d.Delivery_Name,
                    Delivery_Second_Name = d.Delivery_Second_Name,
                    Delivery_Father_Name = d.Delivery_Father_Name,
                    Email = d.Email,
                    Phone_number = d.Phone_number,
                    contactWithMe = d.contactWithMe,
                    noteForOrder = d.noteForOrder,
                    StoreOrDoorDelivery = d.StoreOrDoorDelivery,
                    RecipientWarehouseIndex = d.RecipientWarehouseIndex
                }).FirstOrDefault(d => d.Order_id == item.Order_id);

                item.Order_Details = _orderDetailService.GetOrderDetailByOrderId(item.Order_id, lang);
            }
            return list; 
        }

        public List<OrderVMDTO> GetOrderListByUserID(string UserID, int lang)
        {
           
            List<OrderVMDTO> list = GetAllOrderList(lang).Where(o => o.User != null && o.User.Id.Equals(UserID)).ToList();
            return list;
        }

        public void UpdateOrderStatusByPaimentApp(int status, string invoiceId)
        {
            Order order = _db.Orders.FirstOrDefault(o => o.invoiceId.Equals(invoiceId));
            order.Status = (OrderEnum)status;
            _db.SaveChanges();
        }
        public void UpdateOrderStatus(int status, int orderId)
        {
            Order order = _db.Orders.FirstOrDefault(o => o.Order_id == orderId);
            order.Status = (OrderEnum)status;
            _db.SaveChanges();
        }

        public void SetInvoiceId(int OrderId, string invoiceId)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Order_id == OrderId);
            order.invoiceId = invoiceId;
            _db.Update(order);
            _db.SaveChanges();
        }

        public OrderVMDTO GetCurrentOrder(int lang, int OrderID)
        {
            OrderVMDTO item = GetAllOrderList(lang).FirstOrDefault(o => o.Order_id == OrderID);
            return item;
        }
    }
}
