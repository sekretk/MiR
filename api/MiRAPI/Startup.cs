using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiRAPI.DataModel;
using MiRAPI.Extentions;

namespace MiRAPI
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
            services.AddControllers();

            DataConnection.DefaultSettings = new MiRDataSettings(Configuration.GetConnectionString("MiRDatabase"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Program>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.ConfigureExceptionHandler(logger);

            // use cors to allow all
            app.UseCors(builder => builder
                .WithOrigins("*")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/auth/login"), appBuilder =>    
                                                                    appBuilder.UseMiddleware<Microsoft.AspNetCore.Authentication.AuthenticationMiddleware>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
