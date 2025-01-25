using System.ComponentModel.DataAnnotations;

namespace Jewelery.ViewModels.DTO.Identity
{
    public class RegisterDTOVM
    {
        [Required(ErrorMessage = "First_Name is required")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Second_Name is required")]
        public string Second_Name { get; set; }
        public string? Father_Name { get; set; }
        [Required(ErrorMessage = "Phone_number is required")]
        public string Phone_number { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password",ErrorMessage = "ConfirmPassword is not equal to password")]
        public string ConfirmPassword { get; set; }



    }
}
