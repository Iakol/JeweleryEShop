using Microsoft.AspNetCore.Identity;

namespace Jewelery.Models.Identity_model
{
    public class User : IdentityUser
    {

        public string Name { get; set; }
        public string Second_Name { get; set; }
        public string? Father_Name { get; set; }


    }
}
