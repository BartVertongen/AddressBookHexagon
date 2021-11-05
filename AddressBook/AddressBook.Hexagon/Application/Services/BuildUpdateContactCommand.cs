//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Hexagon.Application.Services
{
    public class UpdateContactCommandBuilderService: IUpdateContactCommandBuilderService
    {
        public IUpdateContactCommandBuilderDTO AddName(string name, IUpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oAdapter;
            IUpdateContactCommandBuilder oBuilder;

            oAdapter = new UpdateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            oBuilder.AddName(name);
            return oAdapter.MapTo(oBuilder);
        }

        public IUpdateContactCommandBuilderDTO AddStreet(string street, IUpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oAdapter;
            IUpdateContactCommandBuilder oBuilder;

            oAdapter = new UpdateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddStreet(street);
            return oAdapter.MapTo(oBuilder);
        }

        public IUpdateContactCommandBuilderDTO AddPostalCode(string postalCode, IUpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oAdapter;
            IUpdateContactCommandBuilder oBuilder;

            oAdapter = new UpdateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddPostalCode(postalCode);
            return oAdapter.MapTo(oBuilder);
        }

        public IUpdateContactCommandBuilderDTO AddTown(string town, IUpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oAdapter;
            IUpdateContactCommandBuilder oBuilder;

            oAdapter = new UpdateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddTown(town);
            return oAdapter.MapTo(oBuilder);
        }

        public IUpdateContactCommandBuilderDTO AddPhone(string phone, IUpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oAdapter;
            IUpdateContactCommandBuilder oBuilder;

            oAdapter = new UpdateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddPhone(phone);
            return oAdapter.MapTo(oBuilder);
        }

        public IUpdateContactCommandBuilderDTO AddEmail(string email, IUpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oAdapter;
            IUpdateContactCommandBuilder oBuilder;

            oAdapter = new UpdateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddEmail(email);
            return oAdapter.MapTo(oBuilder);
        }

        public IUpdateContactCommandDTO Build(string email, IUpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oAdapter;
            IUpdateContactCommandBuilder oBuilder;

            oAdapter = new UpdateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            return (IUpdateContactCommandDTO)oBuilder.Build();
        }
    }
}