namespace WebDoDienTu.Service.PayPalPayment
{
    public class PayPalPaymentResponse
    {
        public string id { get; set; }       
        public string state { get; set; }     
        public string payerId { get; set; }     
        public decimal amount { get; set; }   
        public string currency { get; set; }    
        public string paymentMethod { get; set; }
    }
}
