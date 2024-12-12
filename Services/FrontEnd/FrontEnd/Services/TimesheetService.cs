using FrontEnd.Models.Timesheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.Json;

namespace FrontEnd.Services
{
	public class TimesheetService
	{
		private readonly HttpClient _httpClient;

		public TimesheetService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> Update(Guid id, UpdatingTimeTrackerModel timeTrackingModel)
		{
			var response = await _httpClient.PutAsJsonAsync($"https://localhost:7010/TimeTracker/{id}", timeTrackingModel);

			return response != null && response.IsSuccessStatusCode;
		}

		public async Task<List<TimeTrackerModel>> CreateTimesheetData(List<CreatingTimeTrackerModel> timeTrackingModel)
		{
			var response = await _httpClient.PostAsJsonAsync($"https://localhost:7010/TimeTracker/CreateRangeAsync", timeTrackingModel);

            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<TimeTrackerModel>>(list, options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException("Timesheet service is not available.");
            }
        }

		public async Task<bool> Delete(List<Guid> ids)
		{
			var response = await _httpClient.PostAsJsonAsync($"https://localhost:7010/TimeTracker/DeleteRangeAsync", ids);

			return response != null && response.IsSuccessStatusCode;
		}

		public async Task<List<TimeTrackerModel>> GetTimesheetData(TimeTrackerFilterModel filterModel)
		{
			var response = await _httpClient.PostAsJsonAsync($"https://localhost:7010/TimeTracker/list", filterModel);

			if (response.IsSuccessStatusCode)
			{
				var list = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				return JsonSerializer.Deserialize<List<TimeTrackerModel>>(list, options);
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				return null;
			}
			else
			{
				throw new HttpRequestException("Timesheet service is not available.");
			}
		}

		public async Task<List<ProjectModel>> GetProjects()
		{
			var response = await _httpClient.GetAsync($"https://localhost:7010/Project/all");

			if (response.IsSuccessStatusCode)
			{
				var list = await response.Content.ReadAsStringAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				return JsonSerializer.Deserialize<List<ProjectModel>>(list, options);
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				return null;
			}
			else
			{
				throw new HttpRequestException("Timesheet service is not available.");
			}
		}

		public async Task<Guid> CreateProject(CreatingProjectModel model)
		{
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7010/Project/create", model);

            if (response.IsSuccessStatusCode)
            {
                var id = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<Guid>(id, options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Guid.Empty;
            }
            else
            {
                throw new HttpRequestException("Timesheet service is not available.");
            }
        }

    }
}
