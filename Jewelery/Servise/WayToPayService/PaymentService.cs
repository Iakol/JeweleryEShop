using Azure.Core.Serialization;
using Jewelery.ViewModels.DTO.Cart_item;
using Jewelery.ViewModels.DTO.MonoPay;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Jewelery.Servise.WayToPayService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private const int CurencyCode = 980;
        private readonly string Token;
        private readonly HttpClient _httpClient;


        public PaymentService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            Token = _configuration["MonoPayCoection:ApiKey"];
        }

        public async Task<InvoiceResponceDTO> createInvoice(decimal TotalPrice)
        {
            string BaseURL = "https://api.monobank.ua/api/merchant/invoice/create";
            string SiteUrl = "https://2827-176-37-106-23.ngrok-free.app";
            string WebHookUrl = SiteUrl + "/Order/PayAppWebHook";



            var request = new
            {
                amount = (int)(TotalPrice * 100),
                ccy = CurencyCode,                
                redirectUrl = SiteUrl,
                webHookUrl = WebHookUrl,
                validity = 3600,
                paymentType = "debit"
            };

            var JsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(JsonRequest, Encoding.UTF8, "application/json");
            content.Headers.Add("X-Token", Token);
            var response = await _httpClient.PostAsync(BaseURL, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var JsonResponce = JsonConvert.DeserializeObject<InvoiceResponceDTO>(result);

                return JsonResponce;

            }
            else {
                throw new Exception(response.Content.ToString());
            
            }
    }

        public Task updateInvoice(decimal TotalPrice)
        {
            throw new NotImplementedException();
        }
    }
}
