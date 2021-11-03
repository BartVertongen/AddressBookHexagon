//By Bart Vertongen copyright 2021.

using System.Reflection;
using PS.AddressBook.Framework.Console;
using PS.AddressBook.Framework.Console.Commands;


namespace PS.AddressBook.Infrastructure.Driving.Console
{
    public class AddressBookConsoleAdapter : IAddressBookCLIService
    {
        private readonly IConsoleUserInterface _UserInterface;
        private readonly IAddressBookUICommandFactory _CommandFactory;

        public AddressBookConsoleAdapter(IConsoleUserInterface userInterface, IAddressBookUICommandFactory commandFactory)
        {
            _UserInterface = userInterface;
            _CommandFactory = commandFactory;
        }

        public void Run()
        {
            Greeting();

            var Response = _CommandFactory.GetCommand("?").Run(out object oResult);

            while (!Response.IsTerminating)
            {
                var input = _UserInterface.ReadValue("> ").ToLower();
                var command = _CommandFactory.GetCommand(input);

                Response = command.Run(out oResult);

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

            _UserInterface.WriteMessage("*********************************************************************************************");
            _UserInterface.WriteMessage("*                                                                                           *");
            _UserInterface.WriteMessage("*                         Welcome to the Address Book System                                *");
            _UserInterface.WriteMessage($"*                                                                                v{version}   *");
            _UserInterface.WriteMessage("*********************************************************************************************");
            _UserInterface.WriteMessage("");
        }
    }
}