using BusinessLogic.Abstractions;
using BusinessLogic.Services;
using DataAccess.Context;
using DataAccess.Repositories;
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
            .AddTransient<INewsService, NewsService>()
            .AddTransient<INewsCommentService, NewsCommentService>()
            .AddTransient<IHashtagNewsService, HashtagNewsService>()
            .AddTransient<IHashtagService, HashtagService>()
            .AddTransient<IEmployeeService, EmployeeService>()
            .AddTransient<IBaseService, BaseService>()
            .AddTransient<IPictureService, PictureService>();
            return serviceCollection;
        }

        /// <summary>
        /// Метод расширения. Регистрация репозиториев
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<INewsRepository, NewsRepository>()
                .AddTransient<INewsCommentRepository, NewsCommentRepository>()
                .AddTransient<IHashtagNewsRepository, HashtagNewsRepository>()
                .AddTransient<IHashtagRepository, HashtagRepository>()
                .AddTransient<IEmployeeRepository, EmployeeRepository>()
                .AddTransient<IBaseRepository, BaseRepository>()
                .AddTransient<ILikedNewsRepository, LikedNewsRepository>()
                .AddTransient<IPictureRepository, PictureRepository>();
            return serviceCollection;
        }
    }
}
