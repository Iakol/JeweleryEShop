using Jewelery.Models.Identity_model;

namespace Jewelery.Models.Cart_Model
{
    public class Cart
    {
        public int Cart_id { get; set; }

        public string? User_id { get; set; }
        public User user { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Update_at { get; set; }
    }
}
