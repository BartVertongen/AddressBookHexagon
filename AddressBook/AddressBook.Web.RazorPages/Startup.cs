//By Bart Vertongen copyright 2021.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Services;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Infrastructure.File;


namespace AddressBook.Web.Razor
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
            services.AddRazorPages();

            // Add logging
            services.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true); //Takes care of ILogger
            }));
            services.AddLogging();

            
            services.AddSingleton(Configuration);   // Add access to generic IConfigurationRoot
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IAddressBookFile, AddressBookXmlFileAdapter>();
            services.AddSingleton<ICreateContactUseCase, CreateContactService>();
            services.AddSingleton<IDeleteContactUseCase, DeleteContactService>();
            services.AddSingleton<IUpdateContactUseCase, UpdateContactService>();
            services.AddSingleton<IGetOverviewQuery, GetOverviewService>();
            services.AddSingleton<IGetContactWithNameQuery, GetContactWithNameService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
