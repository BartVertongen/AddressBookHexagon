//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class UpdateContactCommandBuilderDTOMapper : IMapper<IUpdateContactCommandBuilder, IUpdateContactCommandBuilderDTO>
    {
        public IUpdateContactCommandBuilder MapFrom(IUpdateContactCommandBuilderDTO target)
        {
            IUpdateContactCommandBuilder Result = new UpdateContactCommandBuilder()
                .AddName(target.Name)
                .AddPhone(target.Phone)
                .AddEmail(target.Email)
                .AddStreet(target.Street)
                .AddPostalCode(target.PostalCode)
                .AddTown(target.Town);

            return Result;
        }

        public IUpdateContactCommandBuilderDTO MapTo(IUpdateContactCommandBuilder source)
        {
            IUpdateContactCommandBuilderDTO Result = new UpdateContactCommandBuilderDTO()
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
    }
}