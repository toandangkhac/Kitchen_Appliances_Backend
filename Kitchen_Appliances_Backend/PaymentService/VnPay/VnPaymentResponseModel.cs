﻿namespace Kitchen_Appliances_Backend.PaymentService.VnPay
{
    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public string DatePay { get; set; }
        public string Amount { get; set; }
    }

    public class VnPaymentRequestModel
    {
        public string OrderId { get; set; }
        //public DateTime CreateDate { set; get; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
