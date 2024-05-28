﻿using Kitchen_Appliances_MVC.DTO;
using Kitchen_Appliances_MVC.ViewModels.Account;

namespace Kitchen_Appliances_MVC.Abstractions
{
    public interface IAccountClient 
    {
        Task<AccountDTO> findAccount(string email);

        Task<AuthDTO> login(LoginAuthRequest request);

        Task<bool> ForgotPassword(ForgotPasswordRequest request);

        Task<bool>ResetPassword(ResetPasswordRequest request);

        Task<bool> ChangePassword(ChangePasswordRequest request);
    }
}
