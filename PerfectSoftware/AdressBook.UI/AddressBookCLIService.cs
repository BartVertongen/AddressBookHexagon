﻿// By Bart Vertongen copyright 2021.

using System.Reflection;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI.Commands;


namespace PS.AddressBook.UI
{
    public interface IAddressBookCLIService
    {
        void Run();
    }

    public class AddressBookCLIService : IAddressBookCLIService
    {
        private readonly IConsoleUserInterface _UserInterface;
        private readonly IAddressBookUICommandFactory _CommandFactory;

        public AddressBookCLIService(IConsoleUserInterface userInterface, IAddressBookUICommandFactory commandFactory)
        {
            _UserInterface = userInterface;
            _CommandFactory = commandFactory;
        }

        public void Run()
        {
            Greeting();

            var Response = _CommandFactory.GetCommand("?").Run();

            while (!Response.IsTerminating)
            {
                // look at this mistake with the ToLower()
                var input = _UserInterface.ReadValue("> ").ToLower();
                var command = _CommandFactory.GetCommand(input);

                Response = command.Run();

                if (!Response.WasSuccessful)
                {
                    _UserInterface.WriteMessage("");
                    _UserInterface.WriteWarning("Enter ? to view options.");
                    _UserInterface.WriteMessage("");
                }
            }
        }

        private void Greeting()
        {
            // get version and display
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();            

            _UserInterface.WriteMessage( "*********************************************************************************************");
            _UserInterface.WriteMessage( "*                                                                                           *");
            _UserInterface.WriteMessage( "*                         Welcome to the Address Book System                                *");
            _UserInterface.WriteMessage($"*                                                                                v{version}   *");
            _UserInterface.WriteMessage( "*********************************************************************************************");
            _UserInterface.WriteMessage( "");
        }
    }
}