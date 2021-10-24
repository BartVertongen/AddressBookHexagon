﻿//By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports.Out;


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
            AddressBookDTOMapper oAddressBookDTOMapper = new();
            IList<IContactDTO> oAddressBookDTO = new List<IContactDTO>();

            _AddressBookFilePort.Load(oAddressBookDTO);
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