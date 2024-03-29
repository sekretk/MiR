using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using Microsoft.OpenApi.Models;
using MiRAPI.DataModel;
using MiRAPI.Extentions;
using MiRAPI.utils;
using Swashbuckle.AspNetCore.Swagger;

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
            try
            {
                services.AddControllers().AddNewtonsoftJson();

                services.AddOptions();

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "MiR API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference()
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                Array.Empty<string>()
                            }
                            });
                });

                System.IO.File.AppendAllText(@"log.txt", $"db connection start");

                DataConnection.DefaultSettings = new MiRDataSettings(Configuration.GetConnectionString("MiRDatabase"));

                System.IO.File.AppendAllText(@"log.txt", $"db connection {Configuration.GetConnectionString("MiRDatabase")}");
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"log.txt", $"Error on start {ex.Message} - {ex.StackTrace}");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger().Info("Configure web api");

            var logger = loggerFactory.CreateLogger<Program>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "MiR API V1"));

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
                                                                                                appBuilder.UseMiddleware<Extentions.AuthenticationMiRMiddleware>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
