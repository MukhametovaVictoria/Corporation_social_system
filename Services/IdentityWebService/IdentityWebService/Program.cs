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

            // Вызов миграции базы данных
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate(); // Применяем миграции
                }
                catch (Exception ex)
                {
                    // Здесь можете добавить логику обработки ошибок
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            host.Run(); // Запускает приложение
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