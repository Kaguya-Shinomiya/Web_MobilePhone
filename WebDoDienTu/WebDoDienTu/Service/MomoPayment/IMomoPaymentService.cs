namespace WebDoDienTu.Service.MomoPayment
{
    public interface IMomoPaymentService
    {
        Task<string> CreatePaymentUrl(MomoPaymentRequestModel model);
        MomoPaymentResponseModel PaymentExecute(IQueryCollection query);
    }
}
