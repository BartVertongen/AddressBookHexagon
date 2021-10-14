//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.UseCases
{
    public interface IDeleteContactUseCase
    {
        /// <summary>
        /// Deletes a Contact from the AddressBook.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>The ContactDTO that will be deleted.</returns>
        IContactDTO DeleteContact(DeleteContactCommand command);
    }
}