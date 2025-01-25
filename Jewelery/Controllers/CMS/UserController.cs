using Jewelery.Infrastructure.Enums;
using Jewelery.Servise.UserService;
using Jewelery.ViewModels.DTO.FillterSession;
using Jewelery.ViewModels.DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Jewelery.Controllers.CMS
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private const String UserFilterStr = "UserFilterStr";

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        public async Task<IActionResult> UserList()
        {
            List<UserListDTOVM> list = await _userService.GetAllUser(1);
            UserFillterDTO UserFilter = new UserFillterDTO
            {
                SearchFilter = new SearchUSerFillterDTO()
            };


            if (HttpContext.Session.GetString(UserFilterStr).IsNullOrEmpty())
            {
                HttpContext.Session.SetString(UserFilterStr, JsonSerializer.Serialize(UserFilter));
            }
            else 
            {
                UserFilter = JsonSerializer.Deserialize<UserFillterDTO>(HttpContext.Session.GetString(UserFilterStr));
            }


            //searchFiltering
            if (!UserFilter.SearchFilter.SearchValue.IsNullOrEmpty()) 
            {
                if (UserFilter.SearchFilter.UserName && UserFilter.SearchFilter.UserEmail && UserFilter.SearchFilter.UserTelephone)
                {
                    list = list.Where(u => (u.user.Name +" "+ u.user.Second_Name + " " + u.user.Father_Name).Contains(UserFilter.SearchFilter.SearchValue) || u.user.Email.Contains(UserFilter.SearchFilter.SearchValue) || u.user.Phone_number.Contains(UserFilter.SearchFilter.SearchValue)).ToList();
                }
                else 
                {
                    List<UserListDTOVM> Filterlist = new List<UserListDTOVM>();
                    if (UserFilter.SearchFilter.UserName)
                    {
                        Filterlist = list.Where(u => (u.user.Name + " " + u.user.Second_Name + " " + u.user.Father_Name).Contains(UserFilter.SearchFilter.SearchValue)).ToList() ;
                        list.RemoveAll(u =>  (u.user.Name + " " + u.user.Second_Name + " " + u.user.Father_Name).Contains(UserFilter.SearchFilter.SearchValue));
                    }
                    if (UserFilter.SearchFilter.UserEmail)
                    {
                        Filterlist = list.Where(u => u.user.Email.Contains(UserFilter.SearchFilter.SearchValue)).ToList();
                        list.RemoveAll(u => u.user.Email.Contains(UserFilter.SearchFilter.SearchValue));
                    }
                    if (UserFilter.SearchFilter.UserTelephone) 
                    {
                        Filterlist = list.Where(u => u.user.Email.Contains(UserFilter.SearchFilter.SearchValue)).ToList();
                        list.RemoveAll(u => u.user.Phone_number.Contains(UserFilter.SearchFilter.SearchValue));
                    }
                    list = Filterlist;

                }         
            }

            //Filtering By Cart And Order
            if (UserFilter.WithItemInCart != null || UserFilter.WithOrder != null || UserFilter.WithUnCloseOrder != null)
            {
                List<UserListDTOVM> Filterlist = new List<UserListDTOVM>();
                //cart Filter
                if (UserFilter.WithItemInCart != null && UserFilter.WithItemInCart == true)
                {
                    list = list.Where(u => u.Cart.cart_ItemDTOVMs != null && u.Cart.cart_ItemDTOVMs.Count() > 0).ToList();
                }
                else if (UserFilter.WithItemInCart != null && UserFilter.WithItemInCart == false) 
                {
                    list = list.Where(u => u.Cart.cart_ItemDTOVMs == null || u.Cart.cart_ItemDTOVMs.Count() == 0).ToList();
                }


                //OrderFilter
                if (UserFilter.WithOrder != null && UserFilter.WithOrder == true && UserFilter.WithUnCloseOrder == null)
                {
                    list = list.Where(u => u.orders.Count() > 0).ToList();
                }
                else if (UserFilter.WithOrder != null && UserFilter.WithOrder == true && UserFilter.WithUnCloseOrder == true) 
                {
                    list = list.Where(u => u.orders.Any(o => o.Status != OrderEnum.Closed)).ToList();
                }
                else if (UserFilter.WithOrder != null && UserFilter.WithOrder == true && UserFilter.WithUnCloseOrder == false)
                {
                    list = list.Where(u => u.orders.All(o => o.Status == OrderEnum.Closed)).ToList();
                }
                else if (UserFilter.WithOrder != null || UserFilter.WithOrder == false)
                {
                    list = list.Where(u => u.orders.Count() == 0).ToList();
                }
                

            }

            
            
            return View(list);
        }

        [HttpPost]
        public IActionResult SetFilterVariable([FromBody] UserFillterDTO UserFilter )
        {
            HttpContext.Session.SetString(UserFilterStr, JsonSerializer.Serialize(UserFilter));
            return Ok(new { success = true });
        }




    }
}
