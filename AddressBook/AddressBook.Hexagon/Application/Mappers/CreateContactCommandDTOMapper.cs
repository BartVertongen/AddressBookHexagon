//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class CreateContactCommandDTOMapper : IMapper<ICreateContactCommand, ICreateContactCommandDTO>
    {
        public ICreateContactCommand MapFrom(ICreateContactCommandDTO target)
        {
            ICreateContactCommand Result;
            CreateContactCommandBuilder oBuilder = new();

            _ = oBuilder.AddName(target.Name).AddEmail(target.Email);
            _ = oBuilder.AddPhone(target.Phone).AddStreet(target.Street);
            _ = oBuilder.AddPostalCode(target.PostalCode).AddTown(target.Town);
            Result = (ICreateContactCommand)oBuilder.Build();
            return Result;
        }

        public ICreateContactCommandDTO MapTo(ICreateContactCommand source)
        {
            ICreateContactCommandDTO Result = new CreateContactCommandDTO()
            {
                Name = source.Name,
                Email = source.Email,
                Phone = source.Phone,
                Street = source.Street,
                PostalCode = source.PostalCode,
                Town = source.Town
            };
            return Result;
        }
    }
}