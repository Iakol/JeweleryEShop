using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Order_model;
using Jewelery.ViewModels.DTO.NovaPost;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jewelery.Servise.NovaPostServise
{
    public class NovaPostServise : INovaPostServise
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string BaseUrl = "https://api.novaposhta.ua/v2.0/json/";
        private readonly string _ApiKey;
        private readonly string _SenderWarehouseIndex;


        public NovaPostServise(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _ApiKey = _configuration["NovaPostConection:ApiKey"];
            _SenderWarehouseIndex = _configuration["NovaPostConection:SenderWarehouseIndex"];
        }

        public async Task<NovaPostCityResponce> GetCityListByName(string CityName)
        {
            var request = new
            {
                apiKey = _ApiKey,
                modelName = "AddressGeneral",
                calledMethod = "searchSettlements",
                methodProperties = new
                {
                    CityName = CityName,
                    Limit = 5,
                    Page = 1
                }
            };

            var JsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync(BaseUrl, content);

            if (responce.IsSuccessStatusCode)
            {

                var result = await responce.Content.ReadAsStringAsync();

                var jsonResponce = JsonConvert.DeserializeObject<NovaPostCityResponce>(result);

                return jsonResponce;

            }
            else
            {
                throw new J_BadRequestExeption();
            }



        }

        public async Task<NovaPostPostResponce> GetPostListByCityRef(string SityRef)
        {
            var request = new
            {
                apiKey = _ApiKey,
                modelName = "AddressGeneral",
                calledMethod = "getWarehouses",
                methodProperties = new
                {
                    CityRef = SityRef,
                    Language = "UA",

                }
            };

            var JsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync(BaseUrl, content);

            if (responce.IsSuccessStatusCode)
            {

                var result = await responce.Content.ReadAsStringAsync();

                var jsonResponce = JsonConvert.DeserializeObject<NovaPostPostResponce>(result);

                return jsonResponce;

            }
            else
            {
                throw new J_BadRequestExeption();
            }
        }

        public async Task<CreateDeliveryDocumentResponce> CreateDeliveryDocument(Order order)
        {
            var request = new {
                SenderWarehouseIndex = _SenderWarehouseIndex,
                RecipientWarehouseIndex = order.Delivery_detail.RecipientWarehouseIndex,
                DateTime = "дОРОБИТИ",
                CargoType = "Parcel",
                Weight = "1", // ReCode
                ServiceType = "WarehouseWarehouse", // after may add DoorTodoor Delivery
                SeatsAmount = order.Order_Details.Count().ToString(),
                Description = "Прикраси",
                Cost = order.Total_price,
                CitySender = "Kyiv",//reCOde
                Sender = 's'
                //REcode

            };
            return new CreateDeliveryDocumentResponce();
        }

        public async Task<CreateDeliveryDocumentResponce> CreateDeliveryDocumentDoorToDoor(Order order)
        {
            var request = new
            {
                SenderWarehouseIndex = _SenderWarehouseIndex,
                RecipientWarehouseIndex = order.Delivery_detail.RecipientWarehouseIndex,
                DateTime = "дОРОБИТИ",
                CargoType = "Parcel",
                Weight = "1", // ReCode
                ServiceType = "WarehouseWarehouse", // after may add DoorTodoor Delivery
                SeatsAmount = order.Order_Details.Count().ToString(),
                Description = "Прикраси",
                Cost = order.Total_price,
                CitySender = "Kyiv",//reCOde
                Sender = 's'
                //REcode

            };
            return new CreateDeliveryDocumentResponce();
        }

        //void INovaPostServiseGetPostListByCityRef(string CityRef)
        //{
        //    throw new NotImplementedException();
        //}

        public void GetOrderStatus()
        {
            throw new NotImplementedException();
        }

        
    }
}
