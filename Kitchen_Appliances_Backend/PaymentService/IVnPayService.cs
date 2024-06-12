namespace Kitchen_Appliances_Backend.PaymentService
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel request);

        VnPaymentResponseModel PaymentExecute(IQueryCollection collection);
    }
}
