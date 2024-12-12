using BusinessLogic.Abstractions;
using BusinessLogic.Services;
using DataAccess.EntityFramework;
using DataAccess.Repositories;
using DataAccess.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Settings;

namespace WebApi
{
    /// <summary>
    /// Регистратор сервиса
    /// </summary>
    public static class Registrar
    {
        /// <summary>
        /// Метод расширения. Вызов цепочки методов IServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettings = configuration.Get<ApplicationSettings>();
            services.AddSingleton(applicationSettings);
            return services.AddSingleton((IConfigurationRoot)configuration)
                .InstallServices()
                .ConfigureContext(applicationSettings.ConnectionString)
                .InstallRepositories();
        }

        /// <summary>
        /// Метод расширения. Регистрация сервисов
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
            .AddTransient<IEmployeeService, EmployeeService>();
            return serviceCollection;
        }

        /// <summary>
        /// Метод расширения. Регистрация репозиториев
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmployeeRepository, EmployeeRepository>();
            return serviceCollection;
        }
    }
}
