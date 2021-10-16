//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Mappers;


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
            AddressBookDTOMapper oAddressBookDTOMapper = new();
            IList<IContactDTO> oAddressBookDTO = new List<IContactDTO>();

            _AddressBookFilePort.Load(oAddressBookDTO);
            oAddressBook = oAddressBookDTOMapper.MapFrom(oAddressBookDTO);
            if (oAddressBook.ContainsName(command.Name))
            {
                IContact FoundContact, ChangedContact;
                ContactDTOMapper oContactDTOMapper = new ();

                FoundContact = oAddressBook.GetContact(command.Name);
                ChangedContact = FoundContact.DeepClone();
                ChangedContact.Name = command.Name;
                if (command.Phone != null) ChangedContact.Phone = command.Phone;
                if (command.Email != null) ChangedContact.Email = command.Email;
                if (command.Street != null) ChangedContact.Address.Street = command.Street;
                if (command.PostalCode != null) ChangedContact.Address.PostalCode = command.PostalCode;
                if (command.Town != null) ChangedContact.Address.Town = command.Town;

                oAddressBook.Update(ChangedContact);
                //Saves the changes to the AddressBook
                _AddressBookFilePort.Save(oAddressBookDTOMapper.MapTo(oAddressBook));
                return oContactDTOMapper.MapTo(ChangedContact);
            }
            else
                return null;
        }
    }
}