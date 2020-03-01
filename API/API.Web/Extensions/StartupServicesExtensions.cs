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
using Microsoft.OpenApi.Models;
using System.Text.Json;

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
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Catalogue API",
                    Version = "v1",
                    Description = "Full API for a catalogue application",
                });
            });
        }

        public static void RegisterApplicationLayers(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContext(configuration);
            services.RegisterUnitOfWork();
            services.RegisterRepositories();
            services.RegisterApplicationServices();
            services.RegisterValidators();
        }

        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<APIDbContext>(options =>
            {
                //options.UseInMemoryDatabase("CatalogueDb");
                options.UseNpgsql(configuration.GetConnectionString("CatalogueDb"), optionsBuilder => optionsBuilder.MigrationsAssembly(@"API.Data"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        private static void RegisterUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
        }

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherService, TeacherService>();
        }

        private static void RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Class>, ClassValidator>();
            services.AddScoped<IValidator<Course>, CourseValidator>();
            services.AddScoped<IValidator<Student>, StudentValidator>();
            services.AddScoped<IValidator<Subject>, SubjectValidator>();
            services.AddScoped<IValidator<Teacher>, TeacherValidator>();
        }
    }
}
