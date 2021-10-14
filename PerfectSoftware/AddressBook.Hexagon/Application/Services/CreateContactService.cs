//By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Ports;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using System.Collections.Generic;

namespace PS.AddressBook.Hexagon.Application.Services
{
    /// <summary>
    /// This is an adapter for the DeleteContactUseCase.
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
            IList<IContactDTO> oAddressBookDTO = new List<IContactDTO>();

            _AddressBookFilePort.Load(oAddressBookDTO);
            oAddressBook = new BussAddressBook(/*oAddressBookDTO*/);

            try
            {
                IContact newContact;
                AdapterToContactDTO ContactAdapter;

                newContact = new Contact(oAddressBook)
                {
                    Name = command.Name,
                    Phone = command.Phone,
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