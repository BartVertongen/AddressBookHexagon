//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    /*public class UpdateContactCommandDTOMapper : IMapper<IUpdateContactCommand, IUpdateContactCommandDTO>
    {
        public IUpdateContactCommand MapFrom(IUpdateContactCommandDTO target)
        {
            IUpdateContactCommand Result = new UpdateContactCommand(target.Name,
                target.Street, target.PostalCode, target.Town, target.Phone, target.Email);


            return Result;
        }

        public IUpdateContactCommandDTO MapTo(IUpdateContactCommand source)
        {
            IUpdateContactCommandDTO Result = new UpdateContactCommandDTO()
            {
                Name = source.Name,
                Phone = source.Phone,
                Email = source.Email,
                Street = source.Street,
                PostalCode = source.PostalCode,
                Town = source.Town
            };
            return Result;
        }
    }*/
}