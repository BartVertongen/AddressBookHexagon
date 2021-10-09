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
using PS.AddressBook.Hexagon.Domain.Core;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Infrastructure.Driven.File;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace WebAPIAddressBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = (IConfigurationRoot)configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            services.AddSingleton<IAddressBook, BussAddressBook>();
            services.AddSingleton<IAddressBookService, AddressBookService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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