using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ViewModels.Account;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_Backend.Commons.Responses;

namespace Kitchen_Appliances_MVC.ApiServices
{
    public class AccountClientService : IAccountClient
    {

        private readonly HttpClient _httpClient;

        public static string Api = "/gateway/account";

        public AccountClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<AccountDTO>> findAccount(string email)
        {
            var account =  await _httpClient.GetFromJsonAsync<APIResponse<AccountDTO>>(Api + "/find-email" + $"/{email}");
            return account;
        }

        public async Task<APIResponse<bool>> ForgotPassword(ForgotPasswordRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<ForgotPasswordRequest>(Api + "/forgot-password", request);
            if(response.IsSuccessStatusCode )
            {
                Console.WriteLine($"Gửi thành công HTTP: ");
                return new APIResponse<bool>()
                {
                    Status = 200,
                    Message = "Gửi email quên mật khẩu",
                    Data = true
                };
            }
            else
            {
                Console.WriteLine($"Lỗi HTTP: ");
                return new APIResponse<bool>()
                {
                    Status = 200,
                    Message = "Gửi email quên mật khẩu",
                    Data = true
                };
            }
        }

        public async Task<APIResponse<AuthDTO>> login(LoginAuthRequest request)
        {

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(Api+ "/login", request);

            return await response.Content.ReadFromJsonAsync<APIResponse<AuthDTO>>();

            //if (response.IsSuccessStatusCode)
            //{
            //    string jsonResponse = await response.Content.ReadAsStringAsync();
            //    var authDto = JsonConvert.DeserializeObject<AuthDTO>(jsonResponse);
            //    return authDto;
            //}
            //else
            //{
            //    Console.WriteLine($"Lỗi HTTP: ");
            //    return null;
            //}
        }

        public async Task<APIResponse<bool>> ResetPassword(ResetPasswordRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<ResetPasswordRequest>(Api + "/reset-password", request);
            return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
            //if (response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine($"Gửi HTTP thành công !!!");
            //    return true;
            //}
            //else
            //{
            //    Console.WriteLine($"Lỗi HTTP: ");
            //    return false;
            //}
        }

        public async Task<APIResponse<bool>> ChangePassword(ChangePasswordRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<ChangePasswordRequest>(Api + "/change-password", request);
            return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
        }

		public Task<APIResponse<bool>> CheckEmail(string Email)
		{

			throw new NotImplementedException();
		}

        public async Task<APIResponse<bool>> ActiveAccount(ActiveAccountRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(Api + "/active-account", request);
            return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
        }

        public async Task<APIResponse<bool>> ResendOTP(ResendOTPRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(Api + "/resend-otp", request);
            return await response.Content.ReadFromJsonAsync<APIResponse<bool>>();
        }
    }
}
