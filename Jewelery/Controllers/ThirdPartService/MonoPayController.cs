using Jewelery.Servise.CartServise;
using Jewelery.Servise.WayToPayService;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.ThirdPartService
{
    public class MonoPayController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ICartServise _cartServise;

        //public JsonContent CreateInvoice() 
        //{

        //}
    }
}
