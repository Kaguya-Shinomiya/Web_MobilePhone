namespace WebDoDienTu.Service.MomoPayment
{
    public class MomoPaymentResponseModel
    {
        public string OrderId { get; set; }
        public string PartnerCode { get; set; }
        public string PaymentUrl { get; set; }
        public string Message { get; set; }
    }
}
