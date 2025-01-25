using Jewelery.Models.Identity_model;
using Jewelery.ViewModels.DTO.Identity;
using Jewelery.ViewModels.DTO.User;
using Microsoft.AspNetCore.Identity;

namespace Jewelery.Servise.IdentityService
{
    public interface IIdentityServise 
    {
        public Task<SignInResult> Login(LogInDTOVM cred);
        public Task Logout();

        public Task<bool> RegisterUser(RegisterDTOVM cred );

        public Task<bool> AddAdmin(RegisterDTOVM cred);
        public Task<IdentityResult> RemoveUser(string id);

        public Task AddRole();

        //public void ResetPassword(); Доробити після Додавання Екстерналу



        public Task<IdentityResult> ChangePassword(User userToChange, String CurrentPassword, String Newpassword);


        public void BindNewUserWithNotAuthOrder(string Phone, string Email, string User_id); // Привязка старих Замовлень які не були Зроблені без реєстрації до нового юзера якщо Співпадають тел і почта

        public string GetUser(HttpContext context);

        public  Task<User?> getUserById(string id);

        //Twiter Facebook and another Auth
    }
}
