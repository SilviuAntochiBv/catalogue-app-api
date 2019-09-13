using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Business.Mappers;
using API.Web.Extensions;

namespace API.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StartupServicesExtensions.AddMvc(services);
            services.AddSwagger();
            services.AddAutoMapper(typeof(StudentMapperProfile).GetTypeInfo().Assembly); // TODO: Modify for your base profile
            services.AddApplicationHealthChecks();
            services.RegisterApplicationLayers(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.ConfigureExceptionHandlerMiddleware(env);
            app.ConfigureMvc();
            app.ConfigureSwagger();
            app.ConfigureHealthChecks();

            app.InitializeDatabase();
        }
    }
}
