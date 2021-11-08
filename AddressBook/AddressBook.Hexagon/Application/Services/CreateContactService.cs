//By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Ports;


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

        public IContactDTO CreateContact(ICreateContactCommandDTO command)
        {
            IAddressBook oAddressBook;
            AddressBookDTOMapper oAddressBookDTOMapper = new();
            IAddressBookDTO oAddressBookDTO = new AddressBookDTO();

            _AddressBookFilePort.Load(ref oAddressBookDTO);
            oAddressBook = oAddressBookDTOMapper.MapFrom(oAddressBookDTO);

            try
            {
                IContact newContact;
                ContactDTOMapper oContactDTOMapper = new();

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
                oAddressBookDTO = oAddressBookDTOMapper.MapTo(oAddressBook);
                _AddressBookFilePort.Save(oAddressBookDTO);
                return oContactDTOMapper.MapTo(newContact);
            }
            catch (Exception)
            {
                throw; //Does it help to improve the stack ?
            }
        }
    }
}