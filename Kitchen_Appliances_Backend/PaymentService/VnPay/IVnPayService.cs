namespace Kitchen_Appliances_Backend.PaymentService.VnPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel request);

        VnPaymentResponseModel PaymentExecute(IQueryCollection collection);
    }
}
