//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class DeleteContactCommandBuilderDTOMapper : IMapper<IDeleteContactCommandBuilder, IDeleteContactCommandBuilderDTO>
    {
        public IDeleteContactCommandBuilder MapFrom(IDeleteContactCommandBuilderDTO target)
        {
            IDeleteContactCommandBuilder Result = new DeleteContactCommandBuilder()
                .AddName(target.Name);

            return Result;
        }

        public IDeleteContactCommandBuilderDTO MapTo(IDeleteContactCommandBuilder source)
        {
            IDeleteContactCommandBuilderDTO Result = new DeleteContactCommandBuilderDTO()
            {
                Name = source.Name,
            };
            return Result;
        }
    }
}