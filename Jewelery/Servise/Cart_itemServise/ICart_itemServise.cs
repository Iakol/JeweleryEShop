using Jewelery.Infrastructure;
using Jewelery.Models.Cart_Model;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Cart_item;
using Jewelery.ViewModels.DTO.Cart_item_option;
using Jewelery.ViewModels.DTO.Product;

namespace Jewelery.Servise.Cart_itemServise
{
    public interface ICart_itemServise
    {
        public ICollection<Cart_item_option> CreateCartItemOption(List<AtributeDTO> Attributes);

        public LocalizationModel GetAtributeDescriptionFromSession(int LocaleSessionId, HttpContext context);

        public Cart_item GetCartItemFromSession(int id, HttpContext context);

        public List<Cart_itemDTOVM> GetCartItemByCartID(int id, int lang);
        public List<Cart_item_optionDTOVM> GetCartItemOption(int id, int lang);


        public void CreateCartItem(ProductDTOVMPage ProductTOCart, Cart cart);
        public void DeleteCartItem(int Cart_item_id);


        public int CopyAtributeName(AtributeDTO atr);

        public Cart_item CreateCartItemForSession(ProductDTOVMPage ProductTOCart, HttpContext context, int id);
        public void DeleteCartItemFromSession(int Cart_item_id, HttpContext context);
        public List<Cart_itemDTOVM> GetCartItemByCartInSession(int lang, HttpContext context);

        public void CreateCartItemFromSessionCart(Cart UserCart, HttpContext context);





    }
}
