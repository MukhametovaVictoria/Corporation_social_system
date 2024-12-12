using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString)
        {
            services
                .AddDbContext<DataContext>(o => o
                    .UseSqlServer(connectionString));
            return services;
        }
    }
}
