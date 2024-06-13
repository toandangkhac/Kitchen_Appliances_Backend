namespace Kitchen_Appliances_Backend.PaymentService.ZaloPay
{
    public class CreateZaloPayRequest
    {
        public CreateZaloPayRequest(int appId, string appUser, long appTime,
           long amount, string appTransId, string bankCode, string description)
        {
            AppId = appId;
            AppUser = appUser;
            AppTime = appTime;
            Amount = amount;
            AppTransId = appTransId;
            BankCode = bankCode;
            Description = description;
        }

        public int AppId { get; set; }
        public string AppUser { get; set; } = string.Empty;
        public long AppTime { get; set; }
        public long Amount { get; set; }
        public string AppTransId { get; set; } = string.Empty;
        public string ReturnUrl { get; }
        public string EmbedData { get; set; } = string.Empty;
        public string Mac { get; set; } = string.Empty;
        public string BankCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


    }
}
