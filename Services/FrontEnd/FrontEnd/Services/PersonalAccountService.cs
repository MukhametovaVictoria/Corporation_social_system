using FrontEnd.Models.PersonalAccountModels;
using FrontEnd.Models.PersonalAccountModels.Employee;
using System.Text.Json;

namespace FrontEnd.Services
{
    public class PersonalAccountService
    {
        private readonly HttpClient _httpClient;

        public PersonalAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Update(UpdatingEmployeeModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5124/api/Employee/Update", model);

            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<EmployeeModelFromPA> GetPersonalAccountData(string id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5124/api/Employee/GetAsync?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result  = JsonSerializer.Deserialize<EmployeeModelFromPA>(user, options);
                return result;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException("PersonalAccount service is not available.");
            }
        }
    }
}
