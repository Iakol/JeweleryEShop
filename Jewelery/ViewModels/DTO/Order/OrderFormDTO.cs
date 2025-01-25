using System.ComponentModel.DataAnnotations;

namespace Jewelery.ViewModels.DTO.Order
{
    public class OrderFormDTO
    {
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? FathertName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool ConnectWithMe { get; set; }

        public string AdditionalInfo { get; set; }

        public string DeliveryOption { get; set; }

        public string City { get; set; }

        public string? WareHouseIndex { get; set; }
        public string Adress { get; set;}
    }
}
