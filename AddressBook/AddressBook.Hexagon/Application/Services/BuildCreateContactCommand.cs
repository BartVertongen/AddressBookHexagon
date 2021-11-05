//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Hexagon.Application.Services
{
    public class CreateContactCommandBuilderService: ICreateContactCommandBuilderService
    {
        public ICreateContactCommandBuilderDTO AddName(string name, ICreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oAdapter;
            ICreateContactCommandBuilder oBuilder;

            oAdapter = new CreateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            oBuilder.AddName(name);
            return oAdapter.MapTo(oBuilder);
        }

        public ICreateContactCommandBuilderDTO AddStreet(string street, ICreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oAdapter;
            ICreateContactCommandBuilder oBuilder;

            oAdapter = new CreateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddStreet(street);
            return oAdapter.MapTo(oBuilder);
        }

        public ICreateContactCommandBuilderDTO AddPostalCode(string postalCode, ICreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oAdapter;
            ICreateContactCommandBuilder oBuilder;

            oAdapter = new CreateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddPostalCode(postalCode);
            return oAdapter.MapTo(oBuilder);
        }

        public ICreateContactCommandBuilderDTO AddTown(string town, ICreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oAdapter;
            ICreateContactCommandBuilder oBuilder;

            oAdapter = new CreateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddTown(town);
            return oAdapter.MapTo(oBuilder);
        }

        public ICreateContactCommandBuilderDTO AddPhone(string phone, ICreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oAdapter;
            ICreateContactCommandBuilder oBuilder;

            oAdapter = new CreateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddPhone(phone);
            return oAdapter.MapTo(oBuilder);
        }

        public ICreateContactCommandBuilderDTO AddEmail(string email, ICreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oAdapter;
            ICreateContactCommandBuilder oBuilder;

            oAdapter = new CreateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            _ = oBuilder.AddEmail(email);
            return oAdapter.MapTo(oBuilder);
        }

        public ICreateContactCommandDTO Build(string email, ICreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oAdapter;
            ICreateContactCommandBuilder oBuilder;

            oAdapter = new CreateContactCommandBuilderDTOMapper();
            oBuilder = oAdapter.MapFrom(builder);
            return (ICreateContactCommandDTO)oBuilder.Build();
        }
    }
}