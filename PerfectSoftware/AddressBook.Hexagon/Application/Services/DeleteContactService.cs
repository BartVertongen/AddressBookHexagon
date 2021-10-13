//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Application.Services
{
    /// <summary>
    /// This is an adapter for the DeleteContactUseCase.
    /// </summary>
    public class DeleteContactService : IDeleteContactUseCase
    {
        private readonly IAddressBookFile _AddressBookFilePort;

        public DeleteContactService(IAddressBookFile file)
        {
            _AddressBookFilePort = file;
        }

        /// <summary>
        /// Deletes a Contact with a given Name.
        /// </summary>
        /// <param name="command">The Delete Command containing the Name to delete.</param>
        /// <returns></returns>
        public IContactDTO DeleteContact(DeleteContactCommand command)
        {
            IAddressBook oAddressBook;
            IAddressBookDTO oAddressBookDTO;

            oAddressBookDTO = new AddressBookDTO();
            _AddressBookFilePort.Load(oAddressBookDTO);
            oAddressBook = new BussAddressBook(_AddressBookFilePort, oAddressBookDTO);
            if (oAddressBook.ContainsName(command.Name))
            {
                IContact FoundContact;
                AdapterToContactDTO Adapter;
               
                FoundContact = oAddressBook.GetContact(command.Name);
                oAddressBook.Delete(command.Name);
                //Saves the changes to the AddressBook
                _AddressBookFilePort.Save(oAddressBookDTO);
                Adapter = new AdapterToContactDTO(FoundContact);
                return Adapter;
            }
            else
                return null;
        }
    }
}