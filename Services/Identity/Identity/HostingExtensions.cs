using BusinessLogic.Services;
using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using test;
using TestIdentity.RabbitMq;

namespace TestIdentity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        //builder.Services.AddRazorPages();
        
        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Identity")));
        
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>();
        

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserManagerWrapper, UserManagerWrapper>();
        builder.Services.AddScoped<ISignInManagerWrapper, SignInManagerWrapper>();
        builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseIdentityServer();
        app.UseAuthorization();

        // Подключение контроллеров
        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}