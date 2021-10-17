//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class AddressDTOMapper : IMapper<IAddress, IAddressDTO>
    {
        public IAddressDTO MapTo(IAddress address)
        {
            IAddressDTO Result = new AddressDTO
            {
                Street = address.Street,
                PostalCode = address.PostalCode,
                Town = address.Town
            };
            return Result;
        }

        public IAddress MapFrom(IAddressDTO addressDTO)
        {
            IAddress Result = new Address(addressDTO.Street,
                                            addressDTO.PostalCode,
                                            addressDTO.Town);
            return Result;
        }
    }
}