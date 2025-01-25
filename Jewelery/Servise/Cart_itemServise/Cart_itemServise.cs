using Jewelery.data;
using Jewelery.Infrastructure;
using Jewelery.Migrations;
using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Cart_item;
using Jewelery.ViewModels.DTO.Cart_item_option;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;

namespace Jewelery.Servise.Cart_itemServise
{
    public class Cart_itemServise : ICart_itemServise
    {
        private readonly AppDBContext _db;
        private readonly IProductServise _productServise;
        private const string CartStringKey = "Cart";
        private const string AtributeDescroptionLocale = "AtributeDescroptionLocale";



        public Cart_itemServise(AppDBContext db, IProductServise productServise)
        {
            _db = db;
            _productServise = productServise;
        }

        public ICollection<Cart_item_option> CreateCartItemOption(List<AtributeDTO> Attributes)
        {
            ICollection<Cart_item_option> List = new HashSet<Cart_item_option>();

            foreach (var Attribute in Attributes)
            {
                List.Add(new Cart_item_option
                {
                     
                    Size = Attribute.Options.FirstOrDefault().Size,
                    Unit = Attribute.Unit,
                    PriceAdjustment = Attribute.Options.FirstOrDefault().PriceAdjustment,
                    Atribute_Description = CopyAtributeName(Attribute) // SQL Процедура яка скопиює локалізацію Атрібуту до ітем Отріон
                });
            }

            return List;

        }

        public ICollection<Cart_item_option> CreateCartItemOptionForSession(List<AtributeDTO> Attributes, HttpContext context)
        {
            ICollection<Cart_item_option> List = new HashSet<Cart_item_option>();

            foreach (var Attribute in Attributes)
            {
                List.Add(new Cart_item_option
                {

                    Size = Attribute.Options.FirstOrDefault().Size,
                    Unit = Attribute.Unit,
                    PriceAdjustment = Attribute.Options.FirstOrDefault().PriceAdjustment,
                    Atribute_Description = CopyAtributeNameForSession(Attribute, context)
                });
            }

            return List;

        }

        public void CreateCartItem(ProductDTOVMPage ProductTOCart, Cart cart)
        {
            Cart_item ToCreate = new Cart_item {
                Cart_id = cart.Cart_id,
                Product_id = ProductTOCart.Product_id,
                Options = CreateCartItemOption(ProductTOCart.Attributes)
            };

            cart.Update_at = DateTime.Now;

            _db.Carts.Update(cart);
            _db.Carts_items.Add(ToCreate);
            _db.SaveChanges();

        }

        public Cart_item CreateCartItemForSession(ProductDTOVMPage ProductTOCart, HttpContext context, int id)
        {
            Cart_item ToCreate = new Cart_item
            {
                Item_id = id,
                Cart_id = 0,
                Product_id = ProductTOCart.Product_id,
                Options = CreateCartItemOptionForSession(ProductTOCart.Attributes, context)
            };

            return ToCreate;
        }

        public void DeleteCartItem(int Cart_item_id)
        {
            Cart_item itemToDelete = _db.Carts_items.Include(o => o.Options).FirstOrDefault(ci => ci.Item_id == Cart_item_id);

            foreach (var item in itemToDelete.Options) {
                LocalizationModel ld = _db.Localizations.Find(item.Atribute_Description);
                _db.Localizations.Remove(ld);
                _db.Cart_item_options.Remove(item);
            }
            _db.Carts_items.Remove(itemToDelete);
            _db.SaveChanges();
        }

        public void DeleteCartItemFromSession(int Cart_item_id, HttpContext context)
        {
            List<Cart_item> CartItems = JsonSerializer.Deserialize<List<Cart_item>>(context.Session.GetString(CartStringKey));
            Cart_item itemToDelete = CartItems.FirstOrDefault(c => c.Item_id == Cart_item_id);

            foreach (var item in itemToDelete.Options)
            {
                List<LocalizationModel> CartLD = JsonSerializer.Deserialize<List<LocalizationModel>>(context.Session.GetString(AtributeDescroptionLocale));  
                LocalizationModel ld = CartLD.FirstOrDefault(l => l.Value_Id == item.Atribute_Description);
                CartLD.Remove(ld);
                context.Session.SetString(AtributeDescroptionLocale, JsonSerializer.Serialize(CartLD));


            }

            CartItems.Remove(itemToDelete);
            context.Session.SetString(CartStringKey, JsonSerializer.Serialize(CartItems));

        }


        public List<Cart_itemDTOVM> GetCartItemByCartID(int id, int lang)
        {
            List < Cart_itemDTOVM > list = new List <Cart_itemDTOVM>();

            var CartItem = _db.Carts_items.Where(ci => ci.Cart_id == id).ToList();
            foreach (var item in CartItem)
            {
                list.Add(new Cart_itemDTOVM
                {
                    Item_id = item.Item_id,
                    Product = _productServise.GetById(item.Product_id, lang),
                    Options = GetCartItemOption(item.Item_id, lang)
                });

            }

            return list;
        }

        public List<Cart_itemDTOVM> GetCartItemByCartInSession(int lang, HttpContext context)
        {
            List<Cart_itemDTOVM> list = new List<Cart_itemDTOVM>();
            
            if (!string.IsNullOrEmpty(context.Session.GetString(CartStringKey)))
            {
                List<Cart_item> CartItems = JsonSerializer.Deserialize<List<Cart_item>>(context.Session.GetString(CartStringKey));
                foreach (var item in CartItems)
                {
                    list.Add(new Cart_itemDTOVM
                    {
                        Item_id = item.Item_id,
                        Product = _productServise.GetById(item.Product_id, lang),
                        Options = GetCartItemOptionFromSession(item.Options.ToList(), lang, context)
                    });
                }
            }            

            return list;
        }

        public Cart_item GetCartItemFromSession(int id, HttpContext context)
        {
            Cart_item item = new Cart_item();

            if (!string.IsNullOrEmpty(context.Session.GetString(CartStringKey)))
            {
                item = JsonSerializer.Deserialize<List<Cart_item>>(context.Session.GetString(CartStringKey)).FirstOrDefault(c => c.Item_id == id);
                
            }

            return item;
        }



        public List<Cart_item_optionDTOVM> GetCartItemOption(int id , int lang)
        {
            SqlParameter Cart_item_id = new SqlParameter("@Cart_item_id", id);
            SqlParameter Language = new SqlParameter("@Language", lang);

            return _db.Set<Cart_item_optionDTOVM>().FromSqlRaw("EXECUTE GetCartItemOption @Cart_item_id,@Language",
                Cart_item_id, Language).ToList();

        }

        public List<Cart_item_optionDTOVM> GetCartItemOptionFromSession(List<Cart_item_option> OptionList, int lang,HttpContext context)
        {

            List<LocalizationModel> localList = JsonSerializer.Deserialize<List<LocalizationModel>>(context.Session.GetString(AtributeDescroptionLocale));
            List<Cart_item_optionDTOVM> OptionDTOLIst = new List<Cart_item_optionDTOVM>();

            foreach (var item in OptionList) 
            {
                switch (lang)
                {
                    case 1:
                        OptionDTOLIst.Add(new Cart_item_optionDTOVM
                        {
                            Cart_item_option_id = item.Cart_item_option_id,
                            Cart_item_id = item.Cart_item_option_id,
                            Atribute_Description = localList.FirstOrDefault(l => l.Value_Id == item.Atribute_Description).UKR,
                            Size = item.Size,
                            Unit = item.Unit,
                            PriceAdjustment = item.PriceAdjustment
                        });
                        break;
                    case 2:
                        OptionDTOLIst.Add(new Cart_item_optionDTOVM
                        {
                            Cart_item_option_id = item.Cart_item_option_id,
                            Cart_item_id = item.Cart_item_option_id,
                            Atribute_Description = localList.FirstOrDefault(l => l.Value_Id == item.Atribute_Description).ENG,
                            Size = item.Size,
                            Unit = item.Unit,
                            PriceAdjustment = item.PriceAdjustment
                        }); break;
                }
            }

            return OptionDTOLIst;
        }

        public int CopyAtributeName(AtributeDTO atr) {

            SqlParameter AttributeId = new SqlParameter("@AtributeId", atr.Atribute_id);
            SqlParameter OutInt = new SqlParameter
            {
                ParameterName = "@DescriptionId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
             _db.Database.ExecuteSqlRaw("EXECUTE CopyAtributeNameToCart_itemOption @AtributeId, @DescriptionId OUTPUT",
                AttributeId, OutInt);
            return Convert.ToInt32(OutInt.Value);
        }

        public int CopyAtributeNameForSession(AtributeDTO atr, HttpContext context)
        {

            Atribute CopyAtr = _db.Atributes.Find(atr.Atribute_id);

            if (string.IsNullOrEmpty(context.Session.GetString(AtributeDescroptionLocale)))
            {
                List<LocalizationModel> CreateLocalizationModelList = new List<LocalizationModel>();
                context.Session.SetString(AtributeDescroptionLocale, JsonSerializer.Serialize(CreateLocalizationModelList));
            }

            List<LocalizationModel> localList = JsonSerializer.Deserialize<List<LocalizationModel>>(context.Session.GetString(AtributeDescroptionLocale));

            int LocaleMaxId = 0;

            if (localList.Count > 0) 
            {
                LocaleMaxId = localList.Max(l => l.Value_Id) + 1;
            }

            LocalizationModel LocalForAtribute = new LocalizationModel
            {
                Value_Id = LocaleMaxId,
                Type_Variable = 8,
                UKR = _db.Localizations.Find(CopyAtr.Atribute_name).UKR,
                ENG = _db.Localizations.Find(CopyAtr.Atribute_name).ENG
            };

            localList.Add(LocalForAtribute);

            context.Session.SetString(AtributeDescroptionLocale, JsonSerializer.Serialize(localList));

            return LocaleMaxId;
        }

        public LocalizationModel GetAtributeDescriptionFromSession(int LocaleSessionId, HttpContext context) 
        {
            List<LocalizationModel> localList = JsonSerializer.Deserialize<List<LocalizationModel>>(context.Session.GetString(AtributeDescroptionLocale));

            LocalizationModel local = localList.FirstOrDefault(l => l.Value_Id == LocaleSessionId);
            return local;
        }

        public void CreateCartItemFromSessionCart( Cart UserCart, HttpContext context) 
        {
            if (string.IsNullOrEmpty(context.Session.GetString(CartStringKey))) 
            {
                List<Cart_item> CartItems = JsonSerializer.Deserialize<List<Cart_item>>(context.Session.GetString(CartStringKey));
                List<LocalizationModel> LocalizationModelList = JsonSerializer.Deserialize<List<LocalizationModel>>(context.Session.GetString(AtributeDescroptionLocale));

                foreach (var item in CartItems) 
                {
                    item.Cart_id = UserCart.Cart_id;
                    _db.Carts_items.Add(item);
                    _db.SaveChanges();
                    foreach (var opr in item.Options) 
                    {                        
                        var loc = LocalizationModelList.FirstOrDefault(l => l.Value_Id == opr.Atribute_Description);
                        if (loc != null) 
                        {
                            _db.Localizations.Add(loc);
                            _db.SaveChanges();
                            opr.Atribute_Description = loc.Value_Id;                        
                        }
                        opr.Cart_item_id = item.Item_id;
                        _db.Cart_item_options.Add(opr);
                        _db.SaveChanges();
                    }                    
                }                
            }
        }

    }
}
