using Jewelery.localization;
using System.ComponentModel.DataAnnotations;

namespace Jewelery.ViewModels.DTO.Identity
{
    public class LogInDTOVM
    {
        [Required(
        ErrorMessageResourceName = nameof(Resource.Email_is_Required),
        ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Required(
        ErrorMessageResourceName = nameof(Resource.Password_is_Required),
        ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }
        public bool isPersistent { get; set; }
    }
}
