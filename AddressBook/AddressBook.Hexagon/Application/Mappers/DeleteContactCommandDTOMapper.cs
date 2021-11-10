//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class DeleteContactCommandDTOMapper : IMapper<IDeleteContactCommand, IDeleteContactCommandDTO>
    {
        public IDeleteContactCommand MapFrom(IDeleteContactCommandDTO target)
        {
            DeleteContactCommandBuilder oBuilder = new();
            IDeleteContactCommand Result = (IDeleteContactCommand)oBuilder.AddName(target.Name).Build();
            return Result;
        }

        public IDeleteContactCommandDTO MapTo(IDeleteContactCommand source)
        {
            IDeleteContactCommandDTO Result = new DeleteContactCommandDTO()
            {
                Name = source.Name,
            };
            return Result;
        }
    }
}