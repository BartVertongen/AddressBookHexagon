
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace AddressBook.Web.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.File("WebAppRazor.log")
                 .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json",
                            optional: true,
                            reloadOnChange: true);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}