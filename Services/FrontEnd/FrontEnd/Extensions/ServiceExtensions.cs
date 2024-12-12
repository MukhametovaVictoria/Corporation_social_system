using FrontEnd.RabbitMq;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FrontEnd.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7272/");
            });
            services.AddHttpClient<NewsService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7175/");
            });
            services.AddHttpClient<PersonalAccountService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5124/");
            });
			services.AddHttpClient<TimesheetService>(client =>
			{
				client.BaseAddress = new Uri("https://localhost:7010/");
			});

			services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = false;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("secret").Value))
                };
            });

            services.AddControllersWithViews();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }
    }
}
