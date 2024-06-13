namespace Kitchen_Appliances_MVC.ViewModels.Account
{
    public class ResendOTPRequest
    {
        public string Email { get; set; }
        //[JsonIgnore]
        public string Type { get; set; }
    }
}
