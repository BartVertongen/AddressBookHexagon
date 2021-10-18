//By Bart Vertongen copyright 2021

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace PS.AddressBook.Hexagon.Application.Services
{
    public class GetContactWithNameService : IGetContactWithNameQuery
    {
        private readonly IAddressBookFile _LoadAddressBookPort;

        public GetContactWithNameService(IAddressBookFile file)
        {
            _LoadAddressBookPort = file;
        }

        public IContactDTO GetContactWithName(string name)
        {
            IContact FoundContact;
            IList<IContactDTO> AddressBookDTO = new List<IContactDTO>();
            AddressBookDTOMapper oAdressBookDTOMapper = new ();
            IAddressBook oAddressBook;

            _LoadAddressBookPort.Load(AddressBookDTO);
            oAddressBook = oAdressBookDTOMapper.MapFrom(AddressBookDTO);
            FoundContact = oAddressBook.GetContact(name);
            if (FoundContact == null)
                return null;
            else
            {
                ContactDTOMapper oContactDTOMapper = new();
                return oContactDTOMapper.MapTo(FoundContact);
            }
        }
    }
}