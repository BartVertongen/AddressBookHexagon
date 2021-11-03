//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Ports;
using BLLAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class AddressBookDTOMapper : IMapper<IAddressBook, IAddressBookDTO>
    {
        public IAddressBookDTO MapTo(IAddressBook addressBook)
        {
            IAddressBookDTO Result = new AddressBookDTO();
            IList<IContact> Contacts;
            ContactDTOMapper oContactDTOMapper = new ();

            Contacts = addressBook.GetOverview("");
            foreach (IContact oContact in Contacts)
            {               
                (Result as List<IContactDTO>).Add(oContactDTOMapper.MapTo(oContact));
            }
            return Result;
        }

        public IAddressBook MapFrom(IAddressBookDTO addressBookDTO)
        {
            IAddressBook Result = new BLLAddressBook();
            ContactDTOMapper oContactDTOMapper = new ();

            foreach (IContactDTO dtoContact in addressBookDTO as List<ContactDTO>)
            {               
                Result.Add(oContactDTOMapper.MapFrom(dtoContact));
            }
            return Result;
        }
    }
}