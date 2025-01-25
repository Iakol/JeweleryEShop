using Jewelery.data;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Identity_model;
using Jewelery.Models.Order_model;
using Jewelery.ViewModels.DTO.Cart;
using Jewelery.ViewModels.DTO.Identity;
using Jewelery.ViewModels.DTO.Order;
using Jewelery.ViewModels.DTO.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;

namespace Jewelery.Servise.IdentityService
{
    public class IdentityServise : IIdentityServise
    {
        private readonly AppDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;


        public IdentityServise(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            AppDBContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<SignInResult> Login(LogInDTOVM cred) {
            
                var result = await _signInManager.PasswordSignInAsync(cred.Email, cred.Password, cred.isPersistent,false);
                return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task AddRole() {
            await _roleManager.CreateAsync(new IdentityRole("Customer"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

        }


        public async Task<bool> RegisterUser(RegisterDTOVM cred )
        {
            User newUser = new User
            {
                Name = cred.First_Name,
                Second_Name = cred.Second_Name,
                Father_Name = cred.Father_Name,
                PhoneNumber = cred.Phone_number,
                Email = cred.Email,
                UserName = cred.UserName
            };

            var result = await _userManager.CreateAsync( newUser, cred.Password );

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Customer");
                BindNewUserWithNotAuthOrder(newUser.PhoneNumber,newUser.Email, newUser.Id);
                return true;
            }
            else {
                string Error = "";
                foreach (IdentityError er in result.Errors) {
                    Error += er.Description + "\n";
                }
                throw new J_BadRequestExeption(Error);
            }


        }

        public async Task<bool> AddAdmin(RegisterDTOVM cred)
        {
            User newUser = new User
            {
                Name = cred.First_Name,
                Second_Name = cred.Second_Name,
                Father_Name = cred.Father_Name,
                PhoneNumber = cred.Phone_number,
                Email = cred.Email,
                UserName = cred.UserName
            };

            var result = await _userManager.CreateAsync(newUser, cred.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return true;
            }
            else
            {
                string Error = "";
                foreach (IdentityError er in result.Errors)
                {
                    Error += er.Description + "\n";
                }
                throw new J_BadRequestExeption(Error);
            }
        }

        public async Task<IdentityResult> RemoveUser(string id)
        {
            User UserToDelete = await _userManager.FindByIdAsync(id);
            return await _userManager.DeleteAsync(UserToDelete);
        }

        public async Task<IdentityResult> ChangePassword(User userToChange,String CurrentPassword, String Newpassword )
        {
            return await _userManager.ChangePasswordAsync(userToChange, CurrentPassword, Newpassword);
        }



        public void BindNewUserWithNotAuthOrder(string Phone, string Email, string User_id)
        {
            var OrderList = _db.Orders.Where(o => (o.User_id == null) && (o.Delivery_detail.Email == Email || o.Delivery_detail.Phone_number == Phone));

            foreach (Order item in OrderList) {
                item.User_id = User_id;
            }
            _db.SaveChanges();
        }

        public string GetUser(HttpContext context) 
        {
            return _userManager.GetUserId(context.User);
        }

        public async Task<User?> getUserById(string id) {

            return await _userManager.FindByIdAsync(id);
        }



    }
}
