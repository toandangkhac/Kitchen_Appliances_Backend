using System.ComponentModel.DataAnnotations;

namespace Kitchen_Appliances_MVC.ViewModels.Account
{
    public class UserAccountModel
    {
        [MaxLength(35)]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool Status { get; set; }

        public int RoleId { get; set; }
    }
}
