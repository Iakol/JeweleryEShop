using Jewelery.data;
using Jewelery.Infrastructure.Enums;
using Jewelery.Models.Identity_model;
using Jewelery.Models.Order_model;
using Jewelery.Models.Product_model;
using Jewelery.Servise.CartServise;
using Jewelery.Servise.IdentityService;
using Jewelery.Servise.OrderService;
using Jewelery.Servise.ProductServise;
using Jewelery.Servise.SessionService;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Jewelery.Controllers.Shop
{
    [Route("Shop/[controller]/[action]")]
    public class CartController : Controller
    {

        private readonly AppDBContext _dbContext;
        private readonly ISessionService _sessionService;
        private readonly ICartServise _cartServise;
        private readonly IIdentityServise _identityServise;
        private readonly IAtributeServise _atributeServise;
        private readonly IProductServise _productServise;
        private readonly IOrderService _orderService;


        public CartController(AppDBContext dbContext, ISessionService sessionService, ICartServise cartServise, IIdentityServise identityServise, IAtributeServise atributeServise, IProductServise productServise, IOrderService orderService)
        {
            _dbContext = dbContext;
            _sessionService = sessionService;
            _cartServise = cartServise;
            _identityServise = identityServise;
            _atributeServise = atributeServise;
            _productServise = productServise;
            _orderService = orderService;
        }

        public IActionResult Cart()
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            CartDTOVM list = new CartDTOVM();
            if (User.Identity.IsAuthenticated)
            {
                list = _cartServise.GetCartByUserDTOVM(_identityServise.GetUser(HttpContext), CultureInt);

            }
            else 
            {
                list = _sessionService.GetCartByUserDTOVM(CultureInt, HttpContext);            
            }
            return View(list);
        }

        public IActionResult CreateOrder() 
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            CartDTOVM list = new CartDTOVM();
            if (User.Identity.IsAuthenticated)
            {
                list = _cartServise.GetCartByUserDTOVM(_identityServise.GetUser(HttpContext), CultureInt);

            }
            else
            {
                list = _sessionService.GetCartByUserDTOVM(CultureInt, HttpContext);
            }
            return View();

        }

        [HttpPost]
        public IActionResult CreateOrder(Delivery_detail delivery_Detail)
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            CartDTOVM list = new CartDTOVM();
            int orderId;
            if (delivery_Detail.contactWithMe == null) 
            {
                delivery_Detail.contactWithMe = false;
            }
            if (User.Identity.IsAuthenticated)
            {
                list = _cartServise.GetCartByUserDTOVM(_identityServise.GetUser(HttpContext), CultureInt);
                orderId = _orderService.PreCreateOrder(list, delivery_Detail, HttpContext, _identityServise.GetUser(HttpContext));

            }
            else
            {
                list = _sessionService.GetCartByUserDTOVM(CultureInt, HttpContext);
                orderId = _orderService.PreCreateOrder(list, delivery_Detail, HttpContext, null);
            }



            return RedirectToAction("PayForOrder", "Order", new { orderId = orderId });

        }

        [HttpPost]
        public IActionResult AddProductToCart([FromBody]ProductDTOVMPage productToadd)
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            List<AtributeDTO> BaseList = _atributeServise.GetAtributeByProduct(productToadd.Product_id, CultureInt);
            List<AtributeDTO> GeneralList = new List<AtributeDTO>();

            foreach (var item in BaseList) 
            {
                var Opt = productToadd
                    .Attributes
                    .FirstOrDefault(a => a.Atribute_id == item.Atribute_id).Options.ToList();
                GeneralList.Add(new AtributeDTO
                {
                    Atribute_id = item.Atribute_id,
                    Atribute_name = item.Atribute_name,
                    Product_id = item.Product_id,
                    Unit = item.Unit,
                    Options = item.Options.Where(o => o.Option_id == Opt[0].Option_id).ToList(),
                    DetermineTheSize_Id = item.DetermineTheSize_Id
                });
            }


            ProductDTOVMPage CreateProductItem = _productServise.GetById(productToadd.Product_id, CultureInt);
            CreateProductItem.Attributes = GeneralList;

            if (User.Identity.IsAuthenticated && User.IsInRole("Customer"))
            {
                _cartServise.AddItemToCart(CreateProductItem, _identityServise.GetUser(HttpContext));
                return null;

            }
            else if (!User.Identity.IsAuthenticated)
            {
                _sessionService.AddItemToCart(CreateProductItem, HttpContext);
                return null;

            }
            else if (User.Identity.IsAuthenticated && !User.IsInRole("CUSTOMER"))
            {
                return null;

            }
            else 
            {
                return null;
            }

        }

        public IActionResult Order() 
        {
            int CultureInt = (int)((LanguageEnums)Enum.Parse(typeof(LanguageEnums), HttpContext.Request.Cookies["Culture"]));
            CartDTOVM list = new CartDTOVM();
            if (User.Identity.IsAuthenticated)
            {
                list = _cartServise.GetCartByUserDTOVM(_identityServise.GetUser(HttpContext), CultureInt);

            }
            else
            {
                list = _sessionService.GetCartByUserDTOVM(CultureInt, HttpContext);
            }
            return View(list);
        }

        public IActionResult DeleteFromCart(int id) 
        {
            if (User.Identity.IsAuthenticated)
            {
                _cartServise.DeleteItemFromCart(id);
            }
            else 
            {
                _sessionService.DeleteItemFromCart(id, HttpContext);     
            }
            return RedirectToAction(nameof(Cart));
        }


    }
}
