using System.ComponentModel.DataAnnotations;

namespace Kitchen_Appliances_Backend.DTO.Account
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
