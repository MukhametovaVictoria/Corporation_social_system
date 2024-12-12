using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
	public static class EntityFrameworkInstaller
	{
		public static IServiceCollection ConfigureContext(this IServiceCollection services,
			string connectionString)
		{
			services.AddDbContext<DataContext>(optionsBuilder
				=> optionsBuilder.UseNpgsql(connectionString));//, b => b.MigrationsAssembly("WebApi")));

			return services;
		}

	}
}
