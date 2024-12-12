using System.Collections;
using System.Collections.Generic;

namespace WebApi.Settings
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }
        public string SiteUrl { get; set; }
        public List<string> Queues { get; set; }
        public string EmployeeFromAuthServiceQueue { get; set; }
        public string HostName { get; set; }

        public ApplicationSettings() {
            ConnectionString = "Server=localhost;Database=Employees;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;encrypt=false;";
            SiteUrl = "https://localhost:7177";
            HostName = "localhost";
            Queues = new List<string>() { "EmployeeRangeForPersonalAccount", "EmployeeRangeForNewsFeed", "EmployeeRangeForTimesheet" };
            EmployeeFromAuthServiceQueue = "AuthUsersQueue";
        }
    }
}
