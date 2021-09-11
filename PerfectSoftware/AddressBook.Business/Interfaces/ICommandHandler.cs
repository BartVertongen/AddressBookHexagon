// By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Business.Interfaces
{
    public interface ICommandHandler
    {
        ICommandResponse Handle(ICommand cmdToHandle);
    }
}