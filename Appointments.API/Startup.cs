using System.Text.Json;
using Appointments.API.Helpers;
using Appointments.Application.Configuration;
using Appointments.Application.Configuration.Emails;
using Appointments.Infrastructure.AppStart;
using Appointments.Utilities.DependencyInjection;
using Appointments.Utilities.Helpers;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Appointments.API
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        #region Properties

        /// <summary>
        /// The solution environment name
        /// </summary>
        readonly string solutionEnvironmentName;
        /// <summary>
        /// ConnectionStringName
        /// </summary>
        readonly string ConnectionStringName = "AppointmentEnities";

        /// <summary>
        /// ILogger
        /// </summary>
        private static ILogger logger;

        /// <summary>
        /// IConfiguration
        /// </summary>
        public IConfiguration appConfig { get; }

        /// <summary>
        /// Service Collection
        /// </summary>
        private IServiceCollection serviceCollection;
        #endregion

        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            logger = LoggerConfig.Configure();
            logger.Information("Logger configured");
            appConfig = configuration;
            solutionEnvironmentName = configuration["ENVIRONMENT"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(appConfig);
            services.Configure<NotificationTimerConfig>(appConfig.GetSection("NotificationTimerConfiguration"));
            services.Configure<MailConfig>(appConfig.GetSection("MailSettings"));

            // Add swagger documentation
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Elekta Coding Challenge API(Appointments)",
                    Version = "v1",
                    Description = "Elekta Coding Challenge using .NET Core REST API CQRS implementation with raw SQL (Dapper) and DDD in line with Clean Architecture."
                });
            });

            services.AddMvc()
              .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                  options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                  options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
              }); // json data output serialisation setings

            var connString = appConfig.GetValue<string>(ConnectionStringName);


            services.AddHttpContextAccessor();

            ApplicationStartup.Initialize(services, connString);

            
            services.AddControllers();

            // set this at the bottom of the page after all services are added.
            serviceCollection = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider(); // Consider alternatives such as dependency injecting services as parameters to 'Configure'.
            DependencyResolver.SetResolver(serviceProvider);  // Set up the resolver that is accessible with all the services

            app.UseMiddleware<ConcurrencyMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint.                  
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Appointments API"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
