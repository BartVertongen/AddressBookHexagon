//By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Application.Services
{
    /// <summary>
    /// This is an adapter for the DeleteConactUseCase.
    /// </summary>
    public class CreateContactService : ICreateContactUseCase
    {
        private readonly IAddressBookFile _AddressBookFilePort;

        public CreateContactService(IAddressBookFile file)
        {
            _AddressBookFilePort = file;
        }

        public IContactDTO CreateContact(CreateContactCommand command)
        {
            IAddressBook oAddressBook;
            IAddressBookDTO oAddressBookDTO = new AddressBookDTO();

            _AddressBookFilePort.Load(oAddressBookDTO);
            oAddressBook = new BussAddressBook(_AddressBookFilePort, oAddressBookDTO);

            try
            {
                IContact newContact;
                AdapterToContactDTO ContactAdapter;

                newContact = new Contact(oAddressBook)
                {
                    Name = command.Name,
                    PhoneNumber = command.Phone,
                    Email = command.Email,
                    Address = new Address()
                    {
                        Street = command.Street,
                        PostalCode = command.PostalCode,
                        Town = command.Town
                    }
                };

                oAddressBook.Add(newContact);

                _AddressBookFilePort.Save(oAddressBookDTO);
                ContactAdapter = new AdapterToContactDTO(newContact);
                return ContactAdapter;
            }
            catch (Exception)
            {
                throw; //Does it help to improve the stack ?
            }
        }
    }
}