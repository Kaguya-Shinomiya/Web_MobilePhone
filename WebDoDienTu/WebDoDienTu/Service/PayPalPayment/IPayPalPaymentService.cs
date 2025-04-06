using PayPal.Api;

namespace WebDoDienTu.Service.PayPal
{
    public interface IPayPalPaymentService
    {
        Payment CreatePayment(decimal totalAmount, string returnUrl, string cancelUrl);
        Payment ExecutePayment(string paymentId, string payerId);
    }
}
