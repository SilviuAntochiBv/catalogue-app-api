using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using API.Business.Implementation.Specific;
using API.Business.Interfaces;
using API.Business.Validators;
using API.Data;
using API.Data.Implementation;
using API.Data.Implementation.Specific;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using API.Domain.Entities;
using System.Diagnostics;

namespace API.Web.Extensions
{
    public static class StartupServicesExtensions
    {
        public static void AddApplicationHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
               .AddCheck("live", () => HealthCheckResult.Healthy(), new[] { "live" })
               .AddCheck("ready", () => HealthCheckResult.Healthy("ready"), new[] { "ready" });
        }

        public static void AddMvc(this IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "WebAPI Template", // TODO: modify
                    Version = "v1", // TODO: modify
                    Description = "WebAPI Template", // TODO: modify
                    TermsOfService = "Terms Of Service"
                });
            });
        }

        public static void RegisterApplicationLayers(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContext(configuration);
            services.RegisterRepositories();
            services.RegisterUnitOfWork();
            services.RegisterValidators();
            services.RegisterApplicationServices();
        }

        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<APIDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("CatalogueDb"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IExampleRepository, ExampleRepository>();
        }

        private static void RegisterUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IExampleService, ExampleService>();
        }

        private static void RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BaseEntity<long>>, ExampleValidator>();
        }
    }
}
