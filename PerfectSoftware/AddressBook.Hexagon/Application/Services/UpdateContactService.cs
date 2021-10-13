//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Application.Services
{
    /// <summary>
    /// This is an adapter for the UpdateContactUseCase.
    /// </summary>
    public class UpdateContactService : IUpdateContactUseCase
    {
        private readonly IAddressBookFile _AddressBookFilePort;

        public UpdateContactService(IAddressBookFile file)
        {
            _AddressBookFilePort = file;
        }

        public IContactDTO UpdateContact(UpdateContactCommand command)
        {
            IAddressBook oAddressBook;
            IAddressBookDTO oAddressBookDTO;

            oAddressBookDTO = new AddressBookDTO();
            _AddressBookFilePort.Load(oAddressBookDTO);
            oAddressBook = new BussAddressBook(_AddressBookFilePort, oAddressBookDTO);
            if (oAddressBook.ContainsName(command.Name))
            {
                IContact FoundContact, ChangedContact;
                AdapterToContactDTO Adapter;

                FoundContact = oAddressBook.GetContact(command.Name);
                ChangedContact = FoundContact.DeepClone();
                ChangedContact.Name = command.Name;
                if (command.Phone != null) ChangedContact.PhoneNumber = command.Phone;
                if (command.Email != null) ChangedContact.Email = command.Email;
                if (command.Street != null) ChangedContact.Address.Street = command.Street;
                if (command.PostalCode != null) ChangedContact.Address.PostalCode = command.PostalCode;
                if (command.Town != null) ChangedContact.Address.Town = command.Town;

                oAddressBook.Update(ChangedContact);
                //Saves the changes to the AddressBook
                _AddressBookFilePort.Save(oAddressBookDTO);
                Adapter = new AdapterToContactDTO(ChangedContact);
                return Adapter;
            }
            else
                return null;
        }
    }
}