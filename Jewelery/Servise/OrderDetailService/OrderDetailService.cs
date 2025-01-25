using Jewelery.data;
using Jewelery.Infrastructure;
using Jewelery.Models.Cart_Model;
using Jewelery.Models.Order_model;
using Jewelery.Models.Product_model;
using Jewelery.Servise.Cart_itemServise;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Cart_item;
using Jewelery.ViewModels.DTO.Cart_item_option;
using Jewelery.ViewModels.DTO.Order_detail;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jewelery.Servise.OrderDetailService
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly AppDBContext _db;
        private readonly ICart_itemServise _cart_itemServise;
        private readonly IProductServise _productServise;

        public OrderDetailService(AppDBContext db, ICart_itemServise cart_itemServise, IProductServise productServise)
        {
            _db = db;
            _cart_itemServise = cart_itemServise;
            _productServise = productServise;
        }

        public void CreateOrderDetails(CartDTOVM Cart,int OrderId, HttpContext context) 
        {
            foreach (var item in Cart.cart_ItemDTOVMs) 
            {
                CreateOrderDetail(item, OrderId, context);
            }
        
        }

        public void CreateOrderDetail(Cart_itemDTOVM item, int OrderId , HttpContext context)
        {
            SqlParameter Order_id = new SqlParameter("@Order_id", OrderId);
            SqlParameter Product_id = new SqlParameter("@Product_id", item.Product.Product_id);
            SqlParameter Product_Photo_URL = new SqlParameter("@Product_Photo_URL", DBNull.Value);
            if (item.Product.Images.Count > 0) 
            {
                Product_Photo_URL = new SqlParameter("@Product_Photo_URL", item.Product.Images[0].Url);
            }

            SqlParameter Order_Detail_id = new SqlParameter 
            {
                ParameterName = "@Order_Detail_id",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction= System.Data.ParameterDirection.Output

            };


            _db.Database.ExecuteSqlRaw("EXECUTE CreateOrderDetail @Order_id,@Product_id,@Product_Photo_URL, @Order_Detail_id OUTPUT",
                Order_id, Product_id, Product_Photo_URL, Order_Detail_id);

            if (item.Options.Count > 0) 
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    CreateOrderOptions(item.Options, (int)Order_Detail_id.Value, context,null);

                }
                else
                {
                    CreateOrderOptions(item.Options, (int)Order_Detail_id.Value, context, _cart_itemServise.GetCartItemFromSession(item.Item_id,context));

                }
            }
        }

        public void CreateOrderOptions(List<Cart_item_optionDTOVM> options, int orderDetailId, HttpContext context, Cart_item? cartItem) 
        {
            foreach (var item in options) 
            {
                SqlParameter Order_detail_id = new SqlParameter("@Order_detail_id", orderDetailId);
                SqlParameter Atribute_Description = new SqlParameter();

                if (context.User.Identity.IsAuthenticated)
                {
                    var locale = _db.Localizations.FirstOrDefault(l => l.Value_Id == _db.Cart_item_options.Find(item.Cart_item_option_id).Atribute_Description);
                    locale.Type_Variable = 9;
                    locale.Value_Id = 0;
                    _db.Localizations.Add(locale);
                    _db.SaveChanges();
                    Atribute_Description = new SqlParameter("@Atribute_Description", locale.Value_Id);

                }
                else {
                    int AtributeDescription = cartItem.Options.FirstOrDefault(o => o.Cart_item_option_id == item.Cart_item_option_id).Atribute_Description;
                    var locale = _cart_itemServise.GetAtributeDescriptionFromSession(AtributeDescription, context);
                    locale.Type_Variable = 9;
                    _db.Localizations.Add(locale);
                    _db.SaveChanges();
                    Atribute_Description = new SqlParameter("@Atribute_Description", locale.Value_Id);
                }

                SqlParameter Size = new SqlParameter("@Size", item.Size);
                SqlParameter Unit = new SqlParameter("@Unit", DBNull.Value);
                if (!item.Unit.IsNullOrEmpty()) 
                {
                    Unit = new SqlParameter("@Unit", item.Unit);
                }
                SqlParameter PriceAdjustment = new SqlParameter("@PriceAdjustment", item.PriceAdjustment);

                _db.Database.ExecuteSqlRaw("EXECUTE CreateOrderOption @Order_detail_id,@Atribute_Description,@Size,@Unit,@PriceAdjustment",
                    Order_detail_id, Atribute_Description, Size, Unit, PriceAdjustment);
            }         
        }

        public List<Order_detailVMDTO> GetOrderDetailByOrderId(int OrderId,int lang) 
        {
            List<Order_detailVMDTO> list = _db.Order_details.Where(o => o.Order_id == OrderId).Select(o => new Order_detailVMDTO 
            {
                Order_detail_id = o.Order_detail_id,
                Order_id = o.Order_id,
                Product_id = o.Product_id,
                Product_Name = null,
                Product_Photo_URL = o.Product_Photo_URL,
                Product = new ViewModels.DTO.Product.ProductDTOVMPage(),
                Options = new List<Order_detail_optionVMDTO>()
            }).ToList();
            foreach (var item in list)
            {
                item.Product = _productServise.GetById(item.Product_id, lang);
                item.Product_Name = item.Product.Name;
                item.Options = GetOptionsVMDTOByOrderDetailId(item.Order_detail_id, lang);
            }

            return list;
        }

        public List<Order_detail_optionVMDTO> GetOptionsVMDTOByOrderDetailId(int DetailId,int lang) 
        {
            SqlParameter Order_detail_id = new SqlParameter("@Order_detail_id", DetailId);
            SqlParameter Language = new SqlParameter("@Language", lang);

            List< Order_detail_optionVMDTO> list = _db.Set< Order_detail_optionVMDTO >().FromSqlRaw("EXECUTE GetOptionsVMDTOByOrderDetailId @Order_detail_id, @Language ", Order_detail_id, Language).ToList();
            return list;


        }


    }
}
