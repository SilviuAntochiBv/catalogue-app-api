﻿using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API.Web.Extensions
{
    public static class StartupConfigurationExtensions
    {
        public static void ConfigureHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/.well-known/live");
            app.UseHealthChecks("/.well-known/ready");
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalogue API");
                c.RoutePrefix = string.Empty;
            });
        }

        public static void ConfigureExceptionHandlerMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                return;
            }

            app.UseExceptionHandler(appBuilder => appBuilder.Run(ExceptionHandler));
        }

        private static RequestDelegate ExceptionHandler =>
            async context =>
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            };

        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<APIDbContext>().Database.Migrate();
            }
        }
    }
}
