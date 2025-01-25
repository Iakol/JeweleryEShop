using Jewelery.ViewModels.DTO.Cart_item;
using Jewelery.ViewModels.DTO.MonoPay;

namespace Jewelery.Servise.WayToPayService
{
    public interface IPaymentService
    {
        public Task<InvoiceResponceDTO> createInvoice(decimal TotalPrice);
        public Task updateInvoice(decimal TotalPrice);
    }
}
