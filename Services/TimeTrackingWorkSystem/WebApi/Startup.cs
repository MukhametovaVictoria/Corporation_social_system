using AutoMapper;
using MassTransit;
using Services.Implementations.Mapping;
using TimeTrackingWorkSystem.Mapping;
using WebApi.Mapping;
using WebApi.RmConsumer;
using WebApi.Settings;

namespace WebApi
{
    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			InstallAutomapper(services);
			services.AddServices(Configuration);
			services.AddControllers();
			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen();

			services.AddMassTransit(x =>
			{
				x.AddConsumer<EventConsumer>();
				x.UsingRabbitMq((context, cfg) =>
				{
					ConfigureRmq(cfg, Configuration);
					RegisterEndPoints(cfg);
				});
			});
			services.AddCors();
            services.AddHostedService<RabbitMqListener>();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseRouting();

			app.UseAuthorization();

			if (!env.IsProduction())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static IServiceCollection InstallAutomapper(IServiceCollection services)
		{
			services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
			return services;
		}

		private static MapperConfiguration GetMapperConfiguration()
		{
			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<ProjectsMappingProfile>();
				cfg.AddProfile<TimeTrackingMappingProfile>();
				cfg.AddProfile<EmployeeMappingProfile>();
				cfg.AddProfile<ProjectsMapping>();
				cfg.AddProfile<TimeTrackersMapping>();
			});
			configuration.AssertConfigurationIsValid();
			return configuration;
		}

		/// <summary>
		/// Конфигурирование RMQ.
		/// </summary>
		/// <param name="configurator"> Конфигуратор RMQ. </param>
		/// <param name="configuration"> Конфигурация приложения. </param>
		private static void ConfigureRmq(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
		{
			var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;
			configurator.Host(rmqSettings.Host,
				rmqSettings.VHost,
				h =>
				{
					h.Username(rmqSettings.Login);
					h.Password(rmqSettings.Password);
				});
		}

		/// <summary>
		/// регистрация эндпоинтов
		/// </summary>
		/// <param name="configurator"></param>
		private static void RegisterEndPoints(IRabbitMqBusFactoryConfigurator configurator)
		{
			configurator.ReceiveEndpoint($"masstransit_event_queue_1", e =>
			{
				e.Consumer<EventConsumer>();
				e.UseMessageRetry(r =>
				{
					r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
				});
				e.PrefetchCount = 1;
				e.UseConcurrencyLimit(1);
			});

		}
	}
}