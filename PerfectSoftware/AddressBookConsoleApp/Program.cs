//By Bart Vertongen copyright 2021.

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Services;
using PS.AddressBook.Framework.Console;
using PS.AddressBook.Framework.Console.Commands;
using PS.AddressBook.Infrastructure.Driving.Console;


namespace PS.AddressBook.ConsoleApp
{
    class Program
    {
        static IConfigurationRoot Configuration;

        private static void ConfigureServices(IServiceCollection services)
        {
            //Add application services.

            //Framework: Add logging
            services.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true); //Takes care of ILogger
            }));
            services.AddLogging();

            //Infrastructure Driven
            //REM: Where is the AddressBookFileAdapter ???
            services.AddSingleton(Configuration);

            //Hexagon
            services.AddTransient<ICreateContactUseCase, CreateContactService>();
            services.AddTransient<IDeleteContactUseCase, DeleteContactService>();
            services.AddTransient<IUpdateContactUseCase, UpdateContactService>();
            services.AddTransient<IGetOverviewQuery, GetOverviewService>();

            //CLI Driving
            services.AddTransient<IConsoleUserInterface, ConsoleUserInterface>();
            services.AddTransient<IAddressBookUICommandFactory, AddressBookUICommandFactory>();
            services.AddTransient<IAddressBookCLIService, AddressBookConsoleAdapter>();          
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

                // 1. Instantiate right-side adapter ("go outside the hexagon")
                // IAddressBookFile FileAdapter = new AddressBookXmlFileAdapter(Configuration);

                // 2. Instantiate the AddressBook Hexagon
                // IAddressBook Hexagon = new AddressBook(FileAdapter);


                // 3. Instantiate the left-side adapter --> AddressBookConsoleAdapter
                //("I want ask/to go inside")
                // IAddressBookCLIService ConsoleAdapter = new AddressBookConsoleAdapter(Hexagon);

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