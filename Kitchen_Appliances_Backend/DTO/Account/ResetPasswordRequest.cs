using System.Text.Json.Serialization;

namespace Kitchen_Appliances_Backend.DTO.Account
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
        [JsonIgnore]
        public string Type { get; set; }
    }
}
