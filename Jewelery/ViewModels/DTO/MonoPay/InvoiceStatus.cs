namespace Jewelery.ViewModels.DTO.MonoPay
{
    public enum MonoInvoiceStatusEnum {
        created,
        processing,
        hold,
        success,
        failure,
        reversed,
        expired
    }
    public class InvoiceStatus
    {
        public string invoiceId { get; set; }
        public MonoInvoiceStatusEnum status { get; set; }
        public string failureReason { get; set; }
        public string errCode { get; set; }
        public string errText { get; set; }
        public string createdDate { get; set; }
        public string modifiedDate { get; set; }


    }
}
