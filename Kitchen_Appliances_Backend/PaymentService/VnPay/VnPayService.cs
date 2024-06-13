using Kitchen_Appliances_Backend.DependencyInjection.Options;
using Kitchen_Appliances_Backend.Helper;
using Org.BouncyCastle.Asn1.X9;

namespace Kitchen_Appliances_Backend.PaymentService.VnPay
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel request)
        {
            var vnpayOptions = new VNPayOptions();
            _configuration.GetSection(nameof(VNPayOptions)).Bind(vnpayOptions);
            var tick = DateTime.Now.Ticks.ToString();
            var vnpay = new VnPayLibrary();
            //vnpay.AddRequestData("vnp_TxnRef", $"{request.OrderId}_{tick}"); //Mã tham chiếu giao dịch
            vnpay.AddRequestData("vnp_TxnRef", $"{request.OrderId}");
            vnpay.AddRequestData("vnp_TmnCode", vnpayOptions.TmnCode);
            vnpay.AddRequestData("vnp_Version", vnpayOptions.Version);
            vnpay.AddRequestData("vnp_Command", vnpayOptions.Command);
            vnpay.AddRequestData("vnp_Amount", (request.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", vnpayOptions.CurrCode);
            vnpay.AddRequestData("vnp_IpAddr", vnpay.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", vnpayOptions.Locale);
            vnpay.AddRequestData("vnp_OrderInfo", $"{request.FullName} {request.Description} {request.Amount}");
            vnpay.AddRequestData("vnp_OrderType", "orther");
            vnpay.AddRequestData("vnp_ReturnUrl", vnpayOptions.PaymentBackReturn);

            var paymentUrl =
                vnpay.CreateRequestUrl(vnpayOptions.BaseUrl, vnpayOptions.HashSecret);
            return paymentUrl;
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collection)
        {
            var vnPay = new VnPayLibrary();
            foreach (var (key, value) in collection)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnPay.AddResponseData(key, value.ToString());
                }
            }
            var vnp_OrderId = Convert.ToInt64(vnPay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnPay.GetResponseData("vnp_TransactionNo"));
            var vnp_ResponseCode = vnPay.GetResponseData("vnp_ResponseCode");
            var vnp_SecureHash =
                collection.FirstOrDefault(k => k.Key == "vnp_SecureHash").Value;
            var vnp_orderInfo = vnPay.GetResponseData("vnp_OrderInfo");

            var checkSignature =
                vnPay.ValidateSignature(vnp_SecureHash, _configuration["VNPayOptions:HashSecret"]);
            if (!checkSignature)
            {
                return new VnPaymentResponseModel()
                {
                    Success = false
                };
            }
            var payTime = vnPay.GetResponseData("vnp_PayDate");
            var amount = vnPay.GetResponseData("vnp_Amount");
            return new VnPaymentResponseModel()
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_orderInfo,
                OrderId = vnp_OrderId.ToString(),
                PaymentId = vnp_TransactionId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode,
                DatePay = payTime,
                Amount = amount.Remove(amount.Length - 3, 2)
            };
        }
    }
}
