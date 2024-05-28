
using System.ComponentModel.DataAnnotations;

namespace Kitchen_Appliances_Backend.DTO.Account;

public class ForgotPasswordRequest
{
    [Display(Name = "Email Address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
}
