namespace PersonalAccountV2.Settings
{
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }
        public string SiteUrl { get; set; }
        public string RabbitMqQueue { get; set; }
        public string HostName { get; set; }

        public ApplicationSettings()
        {
            ConnectionString = "Server=localhost;Database=PersonalAccountV2;MultipleActiveResultSets=true;encrypt=false;User ID=bpm;Password=bpm;";
            SiteUrl = "http://localhost:5124";
            HostName = "localhost";
            RabbitMqQueue = "EmployeeRangeForPersonalAccount";
        }
    }
}
