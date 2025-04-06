using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace WebDoDienTu.Service.MomoPayment
{
    public class MomoPaymentService : IMomoPaymentService
    {
        private readonly IConfiguration _configuration;

        public MomoPaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreatePaymentUrl(MomoPaymentRequestModel model)
        {
            model.OrderId = DateTime.UtcNow.Ticks.ToString();
            model.OrderInfo = model.OrderInfo;

            var rawData =
                $"partnerCode={_configuration["MomoAPI:PartnerCode"]}&accessKey={_configuration["MomoAPI:AccessKey"]}&requestId={model.OrderId}&amount={model.Amount}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&returnUrl={_configuration["MomoAPI:ReturnUrl"]}&notifyUrl={_configuration["MomoAPI:NotifyUrl"]}&extraData=";

            var client = new RestClient(_configuration["MomoAPI:MomoApiUrl"]);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Tạo đối tượng chứa thông tin yêu cầu thanh toán
            var requestData = new
            {
                partnerCode = _configuration["MomoAPI:PartnerCode"],
                accessKey = _configuration["MomoAPI:AccessKey"],
                requestId = model.OrderId,
                amount = model.Amount.ToString(),
                orderId = model.OrderId,
                orderInfo = model.OrderInfo,
                returnUrl = _configuration["MomoAPI:ReturnUrl"],
                notifyUrl = _configuration["MomoAPI:NotifyUrl"],
                requestType = _configuration["MomoAPI:RequestType"],
                signature = ComputeHmacSha256(rawData, _configuration["MomoAPI:SecretKey"])
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            // Kiểm tra nếu response không thành công
            if (!response.IsSuccessful)
            {
                throw new Exception("Lỗi khi thực hiện yêu cầu thanh toán: " + response.Content);
            }

            var responseData = JsonConvert.DeserializeObject<MomoPaymentResponseModel>(response.Content);

            return responseData?.PaymentUrl;
        }


        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        public MomoPaymentResponseModel PaymentExecute(IQueryCollection query)
        {
            return new MomoPaymentResponseModel
            {
                OrderId = query["orderId"],
                PartnerCode = query["partnerCode"],
                PaymentUrl = query["payUrl"],
                Message = query["message"]
            };
        }
    }
}
