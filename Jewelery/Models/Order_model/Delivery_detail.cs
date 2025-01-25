using System.ComponentModel.DataAnnotations;
using Jewelery.localization;
namespace Jewelery.Models.Order_model
{
    public class Delivery_detail
    {

        //Order Credentials
        public int Delivery_detail_id { get; set; }
        public int Order_id { get; set; }
        public Order Order { get; set; }
        public string Delivery_Name { get; set; }
        public string Delivery_Second_Name { get; set; }

        public string? Delivery_Father_Name { get; set; }

        public string Email { get; set; }
        public string Phone_number { get; set; }

        public bool contactWithMe { get; set; }

        public string? noteForOrder { get; set; }


        //Delivery Credentials

        public bool StoreOrDoorDelivery { get; set; } // true - Store : false - Door
        public string? RecipientWarehouseIndex { get; set; }
        
        // Add adress strings 

    }
}
