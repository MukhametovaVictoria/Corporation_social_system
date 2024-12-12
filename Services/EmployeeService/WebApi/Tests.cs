using System;
using System.Collections.Generic;
using System.Text.Json;
using WebApi.Models.Employee;

namespace WebApi
{
    public static class Tests
    {
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
