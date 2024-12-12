using System.Collections.Generic;

namespace DataAccess.Common
{
    public static class Constants
    {
        public static string ProjectName = "NewsFeed";
        public static string ModelsFolderName = "WebApi.Models";
        public static string CommonFolderName = "DataAccess.Common";
        public static string RepositoriesPath = "DataAccess.Repositories";

        public static Dictionary<string, string> TableAndRepositoryPath = new Dictionary<string, string>()
        {
            {"News", $"{RepositoriesPath}.NewsRepository" },
            {"Employee", $"{RepositoriesPath}.EmployeeRepository" },
            {"NewsComment", $"{RepositoriesPath}.NewsCommentRepository"  },
            {"Hashtag", $"{RepositoriesPath}.HashtagRepository" },
            {"HashtagNews", $"{RepositoriesPath}.HashtagNewsRepository"  }
        };

        public static Dictionary<string, string> ClassPathByName = new Dictionary<string, string>()
        {
            {"News", $"{ModelsFolderName}.News.NewsModel" },
            {"Employee", $"{ModelsFolderName}.Employee.EmployeeModel" },
            {"NewsComment", $"{ModelsFolderName}.NewsComment.NewsCommentModel"  },
            {"Hashtag", $"{ModelsFolderName}.Hashtag.HashtagModel" },
            {"HashtagNews", $".{ModelsFolderName}.HashtagNews.HashtagNewsModel"  },
            {"MappingQuery", $"{CommonFolderName}.SqlQuery.MappingQuery"  }
        };
    }
}
