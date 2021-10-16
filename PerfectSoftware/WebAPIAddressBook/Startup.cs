// By Bart Vertongen copyright 2021.

using System.Reflection;
using System.IO;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using PS.AddressBook.Infrastructure.Driven.File;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Services;
using PS.AddressBook.Hexagon.Application.Ports.Out;


namespace WebAPIAddressBook
{
    /// <summary>
    /// Contains all the configuration needed for the startup of the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = (IConfigurationRoot)configuration;
        }

        /// <summary>
        /// Holds a reference to the Settings of the Application.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                    { 
                        Title = "WebAPIAddressBook", 
                        Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Bart Vertongen",
                        Email = "bartvertongen70@gmail.com",
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            // Add logging
            services.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true); //Takes care of ILogger
            }));

            services.AddLogging();

            // Add access to generic IConfigurationRoot
            services.AddSingleton(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);          
            services.AddSingleton<IAddressBookFile, AddressBookXmlFileAdapter>();
            services.AddSingleton<ICreateContactUseCase, CreateContactService>();            
            services.AddSingleton<IDeleteContactUseCase, DeleteContactService>();
            services.AddSingleton<IUpdateContactUseCase, UpdateContactService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIAddressBook v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}