using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace DA.Context
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString)
        {
            services
                .AddDbContext<DataContext>(o => o
                    .UseSqlServer(connectionString, b => b.MigrationsAssembly("PersonalAccountV2")));
            return services;
        }
    }
}
