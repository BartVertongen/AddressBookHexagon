//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Domain;


namespace PS.AddressBook.Hexagon.Framework.Console.Commands
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
            return input.ToLower() switch
            {
                "q" or "quit" => new QuitCommand(_UserInterface),
                "a" or "add" => new AddContactCommand(_AddressBook, _UserInterface),
                "d" or "delete" => new DeleteContactCommand(_AddressBook, _UserInterface),
                "s" or "select" => new SelectContactCommand(_AddressBook, _UserInterface),
                "u" or "update" => new UpdateContactCommand(_AddressBook, _UserInterface, this),
                "v" or "view" => new ViewContactCommand(_AddressBook, _UserInterface),
                "?" or "help" => new HelpCommand(_UserInterface),
                "l" or "list" => new GetOverViewCommand(_AddressBook, _UserInterface),
                _ => new UnknownCommand(_UserInterface),
            };
        }
    }
}