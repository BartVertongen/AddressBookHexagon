//By Bart Vertongen copyright 2021.

using System;
using Microsoft.Extensions.DependencyInjection;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.UI;
using PS.AddressBook.UI.Commands;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.ConsoleApp
{
    class Program
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            // Add application services.
            services.AddTransient<IAddressBook, BussAddressBook>();
            services.AddTransient<IConsoleUserInterface, ConsoleUserInterface>();
            services.AddTransient<IAddressBookUICommandFactory, AddressBookUICommandFactory>();
            services.AddTransient<IAddressBookCLIService, AddressBookCLIService>();          
        }

        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IAddressBookCLIService>();
            service.Run();

            Console.WriteLine("AddressBookService has completed.");
            Console.WriteLine("Press ENTER to exit!");
            Console.ReadLine();
        }
    }
}