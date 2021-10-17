//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Ports;
using BLLAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class AddressBookDTOMapper : IMapper<IAddressBook, IList<IContactDTO>>
    {
        public IList<IContactDTO> MapTo(IAddressBook addressBook)
        {
            IList<IContactDTO> Result = new List<IContactDTO>();
            IList<IContactLineDTO> ContactLines = new List<IContactLineDTO>();
            ContactDTOMapper oContactDTOMapper = new ();

            foreach (IContactLineDTO ContactLine in ContactLines)
            {               
                IContact oContact = addressBook.GetContact(ContactLine.Name);
                Result.Add(oContactDTOMapper.MapTo(oContact));
            }
            return Result;
        }

        public IAddressBook MapFrom(IList<IContactDTO> addressBookDTO)
        {
            IAddressBook Result = new BLLAddressBook();
            ContactDTOMapper oContactDTOMapper = new ();

            foreach (IContactDTO dtoContact in addressBookDTO)
            {               
                Result.Add(oContactDTOMapper.MapFrom(dtoContact));

            }
            return Result;
        }
    }
}