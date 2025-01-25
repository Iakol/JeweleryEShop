using Jewelery.Models.Order_model;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Order;

namespace Jewelery.ViewModels.DTO.User
{
    public class UserListDTOVM
    {
        public UserForAdminManagmentDTOVM user { get; set; }
        public CartDTOVM? Cart { get; set; }

        public int? FavoriteList_ID { get; set; }

        public List<OrderForAdminListDTOVM> orders { get; set; }
        
    }
}
