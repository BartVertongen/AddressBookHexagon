//By Bart Vertongen copyright 2021

using PS.AddressBook.Business.Commands;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.UI.Commands
{
    public interface IAddressBookUICommandFactory
    {
        IUICommand GetCommand(string input);
    }

    public class AddressBookUICommandFactory : IAddressBookUICommandFactory
    {
        private readonly IConsoleUserInterface _UserInterface;
        private readonly IAddressBook _AddressBook;

        public AddressBookUICommandFactory(IAddressBook addressBook, IConsoleUserInterface userInterface)
        {
            _UserInterface = userInterface;
            _AddressBook = addressBook;
        }

        public IUICommand GetCommand(string input)
        {
            switch (input.ToLower())
            {
                case "q":
                case "quit":
                    return new QuitCommand(_UserInterface);
                case "a":
                case "add":
                    return new AddContactCommand(_AddressBook, _UserInterface);
                case "d":
                case "delete":
                    return new DeleteContactCommand(_AddressBook, _UserInterface, this);
                case "s":
                case "select":
                    return new SelectContactCommand(_AddressBook, _UserInterface);
                case "u":
                case "update":
                    return new UpdateContactCommand(_AddressBook, _UserInterface, this);
                case "v":
                case "view":
                    return new ViewContactCommand(_AddressBook, _UserInterface);
                case "?":
                case "help":
                    return new HelpCommand(_UserInterface);
                case "l":
                case "list":
                    return new GetOverViewCommand(_AddressBook, _UserInterface);
                default:
                    return new UnknownCommand(_UserInterface);
            }
        }
    }
}