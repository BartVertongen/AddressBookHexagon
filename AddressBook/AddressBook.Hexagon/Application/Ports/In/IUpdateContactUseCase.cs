//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.UseCases
{
    public interface IUpdateContactUseCase
    {
        IContactDTO UpdateContact(IUpdateContactCommandDTO command);
    }
}