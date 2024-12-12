namespace WebApi.Settings
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }
        public string SiteUrl { get; set; }
        public string RabbitMqQueue { get; set; }
        public string HostName { get; set; }

        public ApplicationSettings() {
            SiteUrl = "https://localhost:7175";
            HostName = "localhost";
            RabbitMqQueue = "EmployeeRangeForNewsFeed";
        }
    }
}
