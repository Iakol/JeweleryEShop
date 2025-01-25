using Jewelery.data;
using Jewelery.Infrastructure.Enums;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Order_model;
using Jewelery.Servise.OrderService;
using Jewelery.ViewModels.DTO.CMSFilter;
using Jewelery.ViewModels.DTO.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;

namespace Jewelery.Controllers.CMS
{
    /*Контролер для Обробки замовлень*/
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrderManagmentController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IOrderService _orderService;
        private const string FilterKey = "OrderCSMFilter";

        public OrderManagmentController(AppDBContext db, IOrderService orderService)
        {
            _db = db;
            _orderService = orderService;
        }

        public IActionResult Index(string? userID)
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            List<OrderVMDTO> list = new List<OrderVMDTO>();
            if (userID == null)
            {
                list = _orderService.GetAllOrderList(CultureInt).OrderBy(o => o.Update_at).ToList();
            }
            else
            {
                list = _orderService.GetOrderListByUserID(userID, CultureInt).OrderBy(o => o.Update_at).ToList();
                if (list.Count() == 0 && list.All(u => u.User.Id.Equals(userID))) {
                    return Redirect(Url.Action("Index", "OrderManagment"));
                }
            }

            //filtering initiate filter
            OrderCMSFilter filter = new OrderCMSFilter();
            if (HttpContext.Session.GetString(FilterKey).IsNullOrEmpty())
            {               
                HttpContext.Session.SetString(FilterKey, JsonSerializer.Serialize(filter));
            }
            else 
            {
                filter = JsonSerializer.Deserialize< OrderCMSFilter >( HttpContext.Session.GetString(FilterKey));
            }

            //filring by search
            if (!filter.SearchValue.IsNullOrEmpty()) 
            {
                if (filter.SearchType != null && filter.SearchType.Count != 0)
                {
                    List<OrderVMDTO> FilterList = new List<OrderVMDTO>();
                    if (filter.SearchType.Contains(0))
                    {
                        FilterList.AddRange(list.Where(o => o.User != null && (o.User.Name + " " + o.User.Second_Name).Contains(filter.SearchValue)).ToList());
                        list.RemoveAll(o => o.User!= null && FilterList.Contains(o));

                    }
                    if (filter.SearchType.Contains(1))
                    {
                        FilterList.AddRange(list.Where(o => (o.Delivery_detail.Delivery_Name + " " + o.Delivery_detail.Delivery_Second_Name).Contains(filter.SearchValue)).ToList());
                        list.RemoveAll(o => FilterList.Contains(o));

                    }
                    if (filter.SearchType.Contains(2))
                    {
                        FilterList.AddRange(list.Where(o => o.Order_Details.Any(d => d.Product_Name.Contains(filter.SearchValue))));
                        list.RemoveAll(o => FilterList.Contains(o));
                    }
                    list = FilterList;
                }
                else
                {
                    list = list.Where(o =>  (o.User != null && (   o.User.Name + " " + o.User.Second_Name).Contains(filter.SearchValue)) ||
                        (o.Delivery_detail.Delivery_Name + " " + o.Delivery_detail.Delivery_Second_Name).Contains(filter.SearchValue) ||
                        o.Order_Details.Any(d => d.Product_Name.Contains(filter.SearchValue))
                    ).ToList();
                }
            }

            //filter By Order Status
            if (filter.SelectedStatus != null && filter.SelectedStatus.Count != 0) 
            {
                list.RemoveAll(o => !filter.SelectedStatus.Contains((int)o.Status));
            }

            //filter by OrderSelect
            if (filter.OrderSelect == 1)
            {
                list = list.OrderBy(l => l.Total_price).ToList();
            }
            else if (filter.OrderSelect == 2)
            {
                list = list.OrderByDescending(l => l.Total_price).ToList();
            }
            else { };

            return View(list);
        }

        [HttpPost]
        public IActionResult SetFilterVariable([FromBody] SetOrderFilterType PartOffilter) 
        {
            OrderCMSFilter filter = JsonSerializer.Deserialize< OrderCMSFilter >( HttpContext.Session.GetString(FilterKey));
            switch (PartOffilter.typeOfFiltering) 
            {
                case 0:
                    filter.SelectedStatus = JsonSerializer.Deserialize<List<int>>(PartOffilter.partOfFilter);
                    break;
                case 1:
                    filter.OrderSelect = int.Parse(PartOffilter.partOfFilter);
                    break;
                case 2:
                    OrderSearchValueDTO searchDTO = JsonSerializer.Deserialize<OrderSearchValueDTO>(PartOffilter.partOfFilter);
                    filter.SearchValue = searchDTO.SearchValue;
                    filter.SearchType = searchDTO.SearchType;
                    break;

            }
            HttpContext.Session.SetString(FilterKey, JsonSerializer.Serialize(filter));
            return null;
        }

        public IActionResult ResetFilters() 
        {
            HttpContext.Session.Remove(FilterKey);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult OrderDetail(int orderId) 
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            var item = _orderService.GetCurrentOrder(CultureInt, orderId);
            return View(item);
        }

        public IActionResult EditDeliveryDetail()
        {
            return View();
        }
        public IActionResult DeleteOrder()
        {
            return View();
        }

        public IActionResult SetOrderStatus(int orderId, int status)
        {
            _orderService.UpdateOrderStatus(status,orderId);
            return RedirectToAction("OrderDetail", new { orderId = orderId } );
        }
    }
}
