using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using WebApi.Models.Employee;
using WebApi.Models.News;
using WebApi.Settings;
using System.Linq;

namespace WebApi
{
    public static class Tests
    {
        public static void TestLikes()
        {
            var applicationSettings = new ApplicationSettings();
            var client = new RestClient(applicationSettings.SiteUrl);
            var req = new RestRequest("Employee/GetListAsync");
            req.AddParameter("page", 1);
            req.AddParameter("itemsPerPage", 10000);
            var r = client.Get(req);

            if (r != null && r.IsSuccessStatusCode)
            {
                var req2 = new RestRequest("News/GetPublishedListAsync");
                req2.AddParameter("page", 1);
                req2.AddParameter("itemsPerPage", 700);
                var r2 = client.Get(req2);

                if (r2 != null && r2.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var list = JsonSerializer.Deserialize<List<EmployeeModel>>(r.Content, options);
                    var news = JsonSerializer.Deserialize<List<NewsModel>>(r2.Content, options);
                    var chunks = news.Chunkify(100).ToList();
                    var tasks = new Task[chunks.Count];
                    for (var j = 0; j < chunks.Count(); j++)
                    {
                        var l = chunks[j].ToList();
                        tasks[j] = Task.Factory.StartNew((Object obj) =>
                        {
                            var listInner = (List<NewsModel>)obj;
                            for (var n = 0; n < listInner.Count(); n++)
                            {
                                var id = listInner[n].Id;
                                for (var i = 0; i < list.Count; i++)
                                {
                                    var request = new RestRequest($"News/Like?employeeId={list[i].Id.ToString()}&newsId={id}", Method.Post);
                                    var response = client.Post(request);
                                }
                            }
                        }, l);
                    }

                    Task.WaitAll(tasks);
                }
            }
        }

        public static string NewMessage()
        {
            var list = new List<EmployeeModel>();
            for (var i = 0; i < 1000; i++)
            {
                list.Add(new EmployeeModel() { Id = Guid.NewGuid(), Firstname = $"Test{i}", Surname = $"Testov{i}" });
            }
            return JsonSerializer.Serialize(list);
        }

        public static IEnumerable<IEnumerable<T>> Chunkify<T>(this IEnumerable<T> source, int size)
        {
            int count = 0;
            using (var iter = source.GetEnumerator())
            {
                while (iter.MoveNext())
                {
                    var chunk = new T[size];
                    count = 1;
                    chunk[0] = iter.Current;
                    for (int i = 1; i < size && iter.MoveNext(); i++)
                    {
                        chunk[i] = iter.Current;
                        count++;
                    }
                    if (count < size)
                    {
                        Array.Resize(ref chunk, count);
                    }
                    yield return chunk;
                }
            }
        }
    }
}
