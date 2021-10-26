

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Services;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Infrastructure.File;


namespace AddressBook.Web.Mvc
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
            // Add logging
            services.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true); //Takes care of ILogger
            }));
            services.AddLogging();

            services.AddControllersWithViews();

            services.AddSingleton(Configuration);   // Add access to generic IConfigurationRoot
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
