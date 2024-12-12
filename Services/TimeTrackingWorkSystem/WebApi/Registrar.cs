using Infrastructure.EntityFramework;
using Infrastructure.Rep.Impl;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using WebApi.Settings;

namespace WebApi
{
	/// <summary>
	/// Регистратор сервиса.
	/// </summary>
	public static class Registrar
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			var applicationSettings = configuration.Get<ApplicationSettings>();
			services.AddSingleton(applicationSettings)
					.AddSingleton((IConfigurationRoot)configuration)
					.InstallServices()
					.ConfigureContext(applicationSettings.ConnectionString)
					.InstallRepositories();
			return services;
		}

		private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
		{
			serviceCollection
				.AddTransient<IProjectService, ProjectsService>()
				.AddTransient<ITimeTrackerService, TimeTrackerService>()
				.AddTransient<IEmployeeService, EmployeeService>();
            return serviceCollection;
		}

		private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
		{
			serviceCollection
				.AddTransient<IProjectRepository, ProjectRepository>()
				.AddTransient<ITimeTrackerRepository, TimeTrackerRepository>()
                .AddTransient<IEmployeeRepository, EmployeeRepository>();
			return serviceCollection;
		}
	}
}