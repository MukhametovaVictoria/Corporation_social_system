using FrontEnd.Models;
using FrontEnd.Models.News;
using FrontEnd.Models.PersonalAccountModels;
using FrontEnd.Models.Picture;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SqlQuery;
using System.Text.Json;

namespace FrontEnd.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7175/api/";

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NewsViewModel> GetAsync(Guid id)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"id", id.ToString()}
            };

            var queryString = string.Join("&", queryParameters
                .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

            var url = $"{_url}News/GetAsync?{queryString}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var newsStr = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var news = JsonSerializer.Deserialize<NewsModel>(newsStr, options);
                if (news != null) {
                    var pics = news.PictureList != null ? news.PictureList : await GetPicturesByNewsIds(new List<Guid>() { news.Id });
                    var nmp = new NewsModelPreparer().SetService(this).SetPicturesList(pics).SetNews(news);
                    var nvmp = new NewsViewModelPreparer().SetNews(news).SetService(this);
                    var newsViewModel = new NewsViewModel();
                    nmp.SetBaseFields(newsViewModel);
                    nmp.SetPictures(newsViewModel);
                    await nmp.SetHashtagNews(newsViewModel); 
                    await nvmp.SetHashtags(newsViewModel);

                    return newsViewModel;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<NewsViewModel>> GetPublishedListAsync(int page, int itemsPerPage, Guid userId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"page", page.ToString()},
                {"itemsPerPage", itemsPerPage.ToString()}
            };

            var queryString = string.Join("&", queryParameters
                .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

            var url = $"{_url}News/GetPublishedListAsync?{queryString}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var news = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var list = JsonSerializer.Deserialize<List<NewsModel>>(news, options);
                return await PrepareNewsList(list, userId);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException("News service is not available.");
            }
        }

        public async Task<List<NewsViewModel>> GetOnModerationListAsync(int page, int itemsPerPage, Guid userId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"page", page.ToString()},
                {"itemsPerPage", itemsPerPage.ToString()}
            };

            var queryString = string.Join("&", queryParameters
                .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

            var url = $"{_url}News/GetOnModerationListAsync?{queryString}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var newsList = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var list = JsonSerializer.Deserialize<List<NewsModel>>(newsList, options);
                var newList = new List<NewsViewModel>();
                if (list != null)
                {
                    foreach (var news in list)
                    {
                        var pics = news.PictureList != null ? news.PictureList : await GetPicturesByNewsIds(new List<Guid>() { news.Id });
                        var nmp = new NewsModelPreparer().SetService(this).SetPicturesList(pics).SetNews(news);
                        var nvmp = new NewsViewModelPreparer().SetNews(news).SetService(this);
                        var newsViewModel = new NewsViewModel();
                        nmp.SetBaseFields(newsViewModel);
                        nmp.SetPictures(newsViewModel);
                        await nmp.SetAuthor(newsViewModel);
                        await nmp.SetHashtagNews(newsViewModel);
                        nvmp.SetIsAuthor(newsViewModel, userId);
                        nvmp.SetAuthorFullName(newsViewModel);
                        await nvmp.SetHashtags(newsViewModel);
                        newList.Add(newsViewModel);
                    }
                }
                return newList;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException("News service is not available.");
            }
        }

        public async Task<EmployeeModel> GetEmployeeInfo(Guid id)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"id", id.ToString()}
            };

            var queryString = string.Join("&", queryParameters
                .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

            var url = $"{_url}Employee/GetAsync?{queryString}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<EmployeeModel>(employee, options);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<LikedNewsInfoModel>> GetLikesInfo(List<Guid> newsIds, Guid currentEmployeeId)
        {
            var data = new {currentEmployeeId, newsIds};
            var response = await _httpClient.PostAsJsonAsync($"{_url}News/GetLikes", data);

            if (response.IsSuccessStatusCode)
            {
                var likes = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<LikedNewsInfoModel>>(likes, options);
            }
            else
            {
                return null;
            }
        }

        public async Task<LikedNewsInfoModel> Like(Guid newsId, Guid currentEmployeeId)
        {
            var newsIds = new List<Guid>() { newsId };
            var data = new { currentEmployeeId, newsIds };
            var response = await _httpClient.PostAsJsonAsync($"{_url}News/Like", data);

            if (response.IsSuccessStatusCode)
            {
                var like = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<LikedNewsInfoModel>(like, options);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<NewsViewModel>> Search(MappingQuery mapping, Guid userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_url}Home/GetSomeCollectionFromMapping", mapping);
            if (response.IsSuccessStatusCode)
            {
                var newsStr = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var news = JsonSerializer.Deserialize<JsonResponseNews>(newsStr, options);
                return await PrepareNewsList(news.Value, userId);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Create(CreatingNewsModel newsModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_url}News/CreateAsync", newsModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<HashtagModel>> GetHashtags(List<Guid> ids)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_url}Hashtag/GetCollection", ids);

            if (response.IsSuccessStatusCode)
            {
                var hashtags = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<HashtagModel>>(hashtags, options);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<HashtagNewsModel>> GetHashtagNewsByNewsIds(List<Guid> newsIds)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_url}HashtagNews/GetCollectionByNewsIds", newsIds);

            if (response.IsSuccessStatusCode)
            {
                var hashtags = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<HashtagNewsModel>>(hashtags, options);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<PictureModel>> GetPicturesByNewsIds(List<Guid> newsIds)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_url}Picture/GetCollectionByNewsIds", newsIds);

            if (response.IsSuccessStatusCode)
            {
                var pics = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<PictureModel>>(pics, options);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Update(UpdatingNewsModel newsModel)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_url}News/SendOnModeration", newsModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid newsId)
        {
            var response = await _httpClient.DeleteAsync($"{_url}News/DeleteAsync?id={newsId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePicture(Guid pictureId)
        {
            var response = await _httpClient.DeleteAsync($"{_url}Picture/DeleteAsync?id={pictureId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CheckIsAuthor(Guid newsId, Guid authorId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"newsId", newsId.ToString()},
                {"authorId", authorId.ToString()}
            };

            var queryString = string.Join("&", queryParameters
                .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

            var url = $"{_url}News/CheckIsAuthor?{queryString}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var news = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<bool>(news, options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }
            else
            {
                throw new HttpRequestException("News service is not available.");
            }
        }

        public async Task<bool> Publish(Guid newsId)
        {
            var response = await _httpClient.PutAsync($"{_url}News/Publish?newsId={newsId}", null);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Archive(Guid newsId)
        {
            var response = await _httpClient.PutAsync($"{_url}News/Archive?newsId={newsId}", null);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Cancel(Guid newsId)
        {
            var response = await _httpClient.PutAsync($"{_url}News/Cancel?newsId={newsId}", null);

            return response.IsSuccessStatusCode;
        }

        private async Task<List<NewsViewModel>> PrepareNewsList(List<NewsModel> list, Guid userId)
        {
            var newsIdsList = new List<Guid>();
            var likes = list.Count > 0 ? await GetLikesInfo(list.Select(x => x.Id).ToList(), userId) : null;
            var pics = list.Count > 0 ? await GetPicturesByNewsIds(list.Select(x => x.Id).ToList()) : null;
            var modelList = new List<NewsViewModel>();
            var employees = new List<EmployeeModel>();
            foreach (var newsItem in list)
            {
                if (newsIdsList.Contains(newsItem.Id))
                    continue;
                else
                    newsIdsList.Add(newsItem.Id);

                var nmp = new NewsModelPreparer().SetService(this).SetPicturesList(pics).SetEmployeesList(employees).SetNews(newsItem);
                var nvmp = new NewsViewModelPreparer().SetNews(newsItem).SetLikesList(likes).SetService(this);
                var facade = new NewsModelFacade(nmp, nvmp, userId);
                await facade.PrepareBase();
                await facade.PrepareAdditional();
                var newNews = facade.GetResult();

                modelList.Add(newNews);
            }
            return modelList;
        }
    }
}
