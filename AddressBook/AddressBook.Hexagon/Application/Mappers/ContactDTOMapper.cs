//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class ContactDTOMapper : IMapper<IContact, IContactDTO>
    {
        public IContactDTO MapTo(IContact contact)
        {
            IContactDTO Result = new ContactDTO
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Address = (AddressDTO)new AddressDTOMapper().MapTo(contact.Address)
            };
            return Result;
        }

        public IContact MapFrom(IContactDTO contactDTO)
        {
            IContact Result = new Contact
            {
                Name = contactDTO.Name,
                Phone = contactDTO.Phone,
                Email = contactDTO.Email,
                Address = (Address)new AddressDTOMapper().MapFrom(contactDTO.Address)
            };               
            return Result;
        }
    }
}