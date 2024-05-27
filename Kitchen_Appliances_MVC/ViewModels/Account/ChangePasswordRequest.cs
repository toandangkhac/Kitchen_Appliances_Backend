using System.ComponentModel.DataAnnotations;

namespace Kitchen_Appliances_MVC.ViewModels.Account
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
