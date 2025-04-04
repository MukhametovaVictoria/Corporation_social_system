using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebService
{
    public class Program
    {
        protected Program() { }
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // ����� �������� ���� ������
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate(); // ��������� ��������
                }
                catch (Exception ex)
                {
                    // ����� ������ �������� ������ ��������� ������
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            host.Run(); // ��������� ����������
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                    });
                });
    }
}