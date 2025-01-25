using Jewelery.data;
using Jewelery.Servise.OrderService;
using Jewelery.Servise.WayToPayService;
using Jewelery.ViewModels.DTO.MonoPay;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.Shop
{
    public class OrderController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly AppDBContext _db;


        public OrderController(IPaymentService paymentService, IOrderService orderService, AppDBContext db )
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PayForOrder(int orderId) 
        {
            var totalPrice = _db.Orders.FirstOrDefault(o => o.Order_id == orderId).Total_price;
            var response = await _paymentService.createInvoice(totalPrice);
            _orderService.SetInvoiceId(orderId, response.invoiceId);
            return Redirect(response.pageUrl);
        }

        public IActionResult ReturnFromPayApp() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult PayAppWebHook([FromBody] InvoiceStatus invoiceStatus)
        {
            if (invoiceStatus.status == (MonoInvoiceStatusEnum)3)
            {
                _orderService.UpdateOrderStatusByPaimentApp(1, invoiceStatus.invoiceId);
            }
            else if (((int)invoiceStatus.status) > 3) 
            {
                _orderService.UpdateOrderStatusByPaimentApp(6, invoiceStatus.invoiceId);
            }
            return null;
        }

        [HttpPost]
        public IActionResult SetStatus(int status, int orderId) 
        {
            _orderService.UpdateOrderStatus(orderId, status);
            return null;
        }



    }
}
