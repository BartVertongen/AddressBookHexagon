//By Bart Vertongen copyright 2021.

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI;
using PS.AddressBook.UI.Commands;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.ConsoleApp
{
    class Program
    {
        static IConfigurationRoot Configuration;

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add application services.
            // Add logging
            services.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true); //Takes care of ILogger
            }));
            services.AddLogging();
            // Add access to generic IConfigurationRoot
            services.AddSingleton(Configuration);
            services.AddTransient<IAddressBook, BussAddressBook>();
            services.AddTransient<IConsoleUserInterface, ConsoleUserInterface>();
            services.AddTransient<IAddressBookUICommandFactory, AddressBookUICommandFactory>();
            services.AddTransient<IAddressBookCLIService, AddressBookCLIService>();          
        }

        private static void Main()
        {
            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.File("AddressBookCLI.log")
                 .CreateLogger();


            // Build configuration
            Log.Information("Creating configuration.");
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

            Log.Information("Creating service collection.");
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            Log.Information("Building service provider");
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Log.Information("Starting service");
            try
            {
                serviceProvider.GetService<IAddressBookCLIService>().Run();
                Log.Information("Ending service");

                Console.WriteLine("AddressBookService has completed.");
                Console.WriteLine("Press ENTER to exit!");
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Log.Fatal(ex, "Error running service");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}