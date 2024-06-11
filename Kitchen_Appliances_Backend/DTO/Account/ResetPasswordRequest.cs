using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kitchen_Appliances_Backend.DTO.Account
{
    public class ResetPasswordRequest
    {
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? OTP { get; set; }
    }
}
