using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Service.VNPayPayment
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
