﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kitchen_Appliances_MVC.ViewModels.Account
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string OTP { get; set; }
        [JsonIgnore]    
        public string Type { get; set; }
    }
}
