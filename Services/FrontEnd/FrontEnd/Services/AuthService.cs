using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Models.Auth;
using FrontEnd.Models.EmployeeService;
using FrontEnd.RabbitMq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Json;
using TestIdentity.Models;

namespace FrontEnd.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IRabbitMqService _rabbitMqService;

        public AuthService(HttpClient httpClient, IRabbitMqService rabbitMqService)
        {
            _httpClient = httpClient;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<UserViewModel> AuthenticateAsync(string username, string password)
        {
            var requestBody = new { username, password };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7272/api/Authentication/login", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<UserViewModel>(user, options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException("Authorization service is not available.");
            }
        }

        public async Task<UserViewModel> GetUser(string username)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7272/api/Users/GetUserByName?name={username}");

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<UserViewModel>(user, options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException("Authorization service is not available.");
            }
        }

        public void SendRegisterRequest(NewUserModel newUser)
        {
            try
            {
                _rabbitMqService.SendMessage(newUser, "RegisterRequestQueue");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("RabbitMQ service is not available.");
            }
        }

        public void SendProblemRequest(LoginProblemRequest newUser)
        {
            try
            {
                _rabbitMqService.SendMessage(newUser, "RegisterProblemRequestQueue");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("RabbitMQ service is not available.");
            }
        }

        public async void LogoutAsync(string id)
        {
            var requestBody = new { id };

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7272/api/Authentication/logout", requestBody);

            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new HttpRequestException("The user is not authorized.");
            }
            else
            {
                throw new HttpRequestException("Authorization service is not available.");
            }
        }

        public async Task<UserViewModel> GetUserByLogin(string username)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7272/api/Users/GetUserByName?name={username}");

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<UserViewModel>(user, options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException("Authorization service is not available.");
            }
        }

        public async Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPasswordModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7272/api/Authentication/forgotPassword", model);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<ForgotPasswordResponseModel>(user, options);
            }
            else
            {
                throw new HttpRequestException("Authorization service is not available.");
            }
        }

        public async Task<ResetPasswordResponseModel> ResetPassword(ResetPasswordModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7272/api/Authentication/resetPassword", model);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<ResetPasswordResponseModel>(user, options);
            }
            else
            {
                throw new HttpRequestException("Authorization service is not available.");
            }
        }
    }
}
