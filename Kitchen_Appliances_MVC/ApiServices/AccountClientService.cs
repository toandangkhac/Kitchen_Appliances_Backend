using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.DTO;
using Kitchen_Appliances_MVC.ViewModels.Account;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Json;

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

        public async Task<AccountDTO> findAccount(string email)
        {
            var account =  await _httpClient.GetFromJsonAsync<AccountDTO>(Api + "/find-email" + $"/{email}");
            return account;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<ForgotPasswordRequest>(Api + "/forgot-password", request);
            
            if(response.IsSuccessStatusCode )
            {
                Console.WriteLine($"Gửi thành công HTTP: ");
                return true;
            }
            else
            {
                Console.WriteLine($"Lỗi HTTP: ");
                return false;
            }
        }

        public async Task<AuthDTO> login(LoginAuthRequest request)
        {

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(Api+ "/login", request);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var authDto = JsonConvert.DeserializeObject<AuthDTO>(jsonResponse);
                Console.WriteLine($"Trả về từ server: {authDto.AccessToken}");
                return authDto;
            }
            else
            {
                Console.WriteLine($"Lỗi HTTP: ");
                return null;
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<ResetPasswordRequest>(Api + "/reset-password", request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Gửi HTTP thành công !!!");
                return true;
            }
            else
            {
                Console.WriteLine($"Lỗi HTTP: ");
                return false;
            }
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest request)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<ChangePasswordRequest>(Api + "/change-password", request);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Gửi HTTP thành công !!!");
                return true;
            }
            else
            {
                Console.WriteLine($"Lỗi HTTP: ");
                return false;
            }
        }
    }
}
