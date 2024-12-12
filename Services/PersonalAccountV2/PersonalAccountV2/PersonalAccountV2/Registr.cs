
using BS.Services.Abstractions;
using PersonalAccountV2.Settings;
using BS.Services.Implementations;
using DA.Repositories.Abstractions;
using DA.Repositories.Implementations;
using DA.Context;


namespace PersonalAccountV2
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
            .AddTransient<IAccomplishmentService, AccomplishmentService>()
            .AddTransient<ICommunicationService, CommunicationService>()
            .AddTransient<IEventService, EventService>()
            .AddTransient<IExperienceService, ExperienceService>()
            .AddTransient<ISkillService, SkillService>()
            .AddTransient<IEmployeeService, EmployeeService>()
            .AddTransient<IBaseService, BaseService>();
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
                .AddTransient<IAccomplishmentRepository, AccomplishmentRepository>()
                .AddTransient<ICommunicationRepository, CommunicationRepository>()
                .AddTransient<IEventRepository, EventRepository>()
                .AddTransient<IExperienceRepository, ExperienceRepository>()
                .AddTransient<IEmployeeRepository, EmployeeRepository>()
                .AddTransient<IBaseRepository, BaseRepository>()
                .AddTransient<ISkillRepository, SkillRepository>();
            return serviceCollection;
        }
    }
}
