using Jewelery.Models.Identity_model;
using Jewelery.Models.Product_model;
using System.ComponentModel.DataAnnotations;

namespace Jewelery.Models.Review
{
    public class Review
    {
        public int Review_id { get; set; }
        public int Product_id { get; set; }
        public Product Product { get; set; }
        public string User_id { get; set; }
        public User User { get; set; }
        [Range(1, 5)]
        public int Raiting { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
    }
}
