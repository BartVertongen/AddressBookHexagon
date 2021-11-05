//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Hexagon.Application.Services
{
    public class DeleteContactCommandBuilderService: IDeleteContactCommandBuilderService
    {
        public IDeleteContactCommandBuilderDTO AddName(string name, IDeleteContactCommandBuilderDTO builder)
        {
            DeleteContactCommandBuilderDTOMapper oAdapter;
            IDeleteContactCommandBuilder oBuilder;

            oAdapter = new DeleteContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            oBuilder.AddName(name);
            return oAdapter.MapTo(oBuilder);
        }

        public IDeleteContactCommandDTO Build(string email, IDeleteContactCommandBuilderDTO builder)
        {
            DeleteContactCommandBuilderDTOMapper oAdapter;
            IDeleteContactCommandBuilder oBuilder;

            oAdapter = new DeleteContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            return (IDeleteContactCommandDTO)oBuilder.Build();
        }
    }
}