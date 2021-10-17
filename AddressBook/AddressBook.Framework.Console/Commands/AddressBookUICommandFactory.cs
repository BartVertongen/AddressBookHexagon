//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.UseCases;


namespace PS.AddressBook.Framework.Console.Commands
{
    public interface IAddressBookUICommandFactory
    {
        IUICommand GetCommand(string input);
    }

    public class AddressBookUICommandFactory : IAddressBookUICommandFactory
    {
        private readonly IConsoleUserInterface  _UserInterface;
        private readonly ICreateContactUseCase  _CreateContactPort;
        private readonly IDeleteContactUseCase  _DeleteContactPort;
        private readonly IUpdateContactUseCase  _UpdateContactPort;
        private readonly IGetOverviewQuery      _GetOverviewPort;
        private readonly IGetContactWithNameQuery _GetContactPort;

        public AddressBookUICommandFactory(ICreateContactUseCase createContactPort,
                                            IDeleteContactUseCase deleteContactPort,
                                            IUpdateContactUseCase updateContactPort,
                                            IGetOverviewQuery getOverviewPort,
                                            IGetContactWithNameQuery getContactPort,
                                                        IConsoleUserInterface userInterface)
        {
            _UserInterface = userInterface;
            _CreateContactPort = createContactPort;
            _DeleteContactPort = deleteContactPort;
            _UpdateContactPort = updateContactPort;
            _GetOverviewPort = getOverviewPort;
            _GetContactPort = getContactPort;
        }

        public IUICommand GetCommand(string input)
        {
            return input.ToLower() switch
            {
                "q" or "quit" => new QuitCommand(_UserInterface),
                "a" or "add" => new AddContactCommand(_CreateContactPort, _UserInterface),
                "d" or "delete" => new DeleteContactCommand(_DeleteContactPort, _GetOverviewPort, _UserInterface),
                "s" or "select" => new SelectContactCommand(_GetOverviewPort, _UserInterface),
                "u" or "update" => new UpdateContactCommand(_UpdateContactPort, _GetContactPort, _UserInterface, this),
                "v" or "view" => new ViewContactCommand(_GetOverviewPort, _UserInterface),
                "?" or "help" => new HelpCommand(_UserInterface),
                "l" or "list" => new GetOverViewCommand(_GetOverviewPort, _UserInterface),
                _ => new UnknownCommand(_UserInterface),
            };
        }
    }
}