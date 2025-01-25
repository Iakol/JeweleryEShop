using Jewelery.data;
using Jewelery.Models.Identity_model;
using Jewelery.Servise.CartServise;
using Jewelery.Servise.OrderDetailService;
using Jewelery.ViewModels.DTO.Order;
using Jewelery.ViewModels.DTO.User;
using Microsoft.AspNetCore.Identity;

namespace Jewelery.Servise.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICartServise _cartServise;

        public UserService(AppDBContext db, UserManager<User> userManager, IOrderDetailService orderDetailService, ICartServise cartServise) {
            _db = db;
            _userManager = userManager;
            _orderDetailService = orderDetailService;
            _cartServise = cartServise;
        }
        public async Task<List<UserListDTOVM>> GetAllUser(int lang) //Нахуя Це ? - Щоб адмін міг глянути Корзину , історію Замовлень, Список Бажань, Коментарії Але це тільки ліст Буде Переход На конкрту Сторнінку Користувача з Всією цією інфою
        {
            var Userlist = await _userManager.GetUsersInRoleAsync("Customer");

            List<UserForAdminManagmentDTOVM> UserDtoList = Userlist.Select(u => new UserForAdminManagmentDTOVM
            {
                Id = u.Id,
                Name = u.Name,
                Second_Name = u.Second_Name,
                Father_Name = u.Father_Name,
                Phone_number = u.PhoneNumber,
                Email = u.Email

            }).ToList();

            List<UserListDTOVM> UserListDTOVM = new List<UserListDTOVM>();

            foreach (UserForAdminManagmentDTOVM item in UserDtoList)
            {
                UserListDTOVM.Add(new UserListDTOVM
                {
                    user = item,
                    Cart = _cartServise.GetCartByUser(item.Id) != null ? _cartServise.GetCartByUserDTOVM(item.Id, lang) : null,
                    FavoriteList_ID = null,
                    orders = (_db.Orders.Where(o => o.User_id == item.Id).ToList()).Select(oDTO => new OrderForAdminListDTOVM
                    {
                        Order_id = oDTO.Order_id,
                        User_id = item.Id,
                        Total_price = oDTO.Total_price,
                        Status = oDTO.Status,
                        OrderItems = _orderDetailService.GetOrderDetailByOrderId(oDTO.Order_id, lang),
                        Created_at = oDTO.Created_at,
                        Update_at = oDTO.Update_at
                    }).ToList()
                });

            } // Більше всього буде рефакториться і додаватися через інші сервіси а не тупо лазити по БД
            return UserListDTOVM;
        }

        public void GetAllAdmin()
        {
            throw new NotImplementedException();
        }
        public void GetUserByIdForAdminManagment()
        {
            throw new NotImplementedException();
        }
    }
}
