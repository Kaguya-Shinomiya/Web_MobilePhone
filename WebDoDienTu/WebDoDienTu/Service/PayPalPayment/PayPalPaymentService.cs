using PayPal.Api;
using System.Globalization;

namespace WebDoDienTu.Service.PayPal
{
    public class PayPalPaymentService : IPayPalPaymentService
    {
        private readonly IConfiguration _configuration;
        private APIContext _apiContext;

        public PayPalPaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            var clientId = _configuration["PayPal:ClientId"];
            var clientSecret = _configuration["PayPal:ClientSecret"];
            var config = new Dictionary<string, string>
            {
                { "mode", _configuration["PayPal:Mode"] }
            };

            var accessToken = new OAuthTokenCredential(clientId, clientSecret, config).GetAccessToken();
            _apiContext = new APIContext(accessToken);
        }

        public Payment CreatePayment(decimal totalAmount, string returnUrl, string cancelUrl)
        {
            var payer = new Payer() { payment_method = "paypal" };
            var itemList = new ItemList() { items = new List<Item>() };

            // Add items to the item list
            itemList.items.Add(new Item()
            {
                name = "Order",
                currency = "USD",
                price = totalAmount.ToString("F2", CultureInfo.InvariantCulture),
                quantity = "1"
            });

            var details = new Details()
            {
                subtotal = totalAmount.ToString("F2", CultureInfo.InvariantCulture)
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = totalAmount.ToString("F2", CultureInfo.InvariantCulture),
                details = details
            };

            var transaction = new Transaction()
            {
                amount = amount,
                description = "Order Description",
                item_list = itemList
            };

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = new List<Transaction>() { transaction },
                redirect_urls = new RedirectUrls()
                {
                    cancel_url = cancelUrl,
                    return_url = returnUrl
                }
            };

            return payment.Create(_apiContext);
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };
            return payment.Execute(_apiContext, paymentExecution);
        }
    }
}
