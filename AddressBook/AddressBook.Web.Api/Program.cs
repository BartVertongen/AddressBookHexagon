//By Bart Vertongen copyright 2021.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace WebAPIAddressBook
{
    /// <summary>
    /// This is the Web Application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This is the method that starts and runs the Web Application.
        /// If this method  stops the application stops.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.File("WebAPIAddressBook.log")
                 .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a HostBuilder and handles the Configuration of the Host.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>The HostBuilder</returns>
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