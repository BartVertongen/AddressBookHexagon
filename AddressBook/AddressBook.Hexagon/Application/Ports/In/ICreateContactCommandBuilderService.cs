//By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface ICreateContactCommandBuilderService
    {
        ICreateContactCommandBuilderDTO AddName(string name, ICreateContactCommandBuilderDTO createCommandDTO);

        ICreateContactCommandBuilderDTO AddStreet(string street, ICreateContactCommandBuilderDTO createCommandDTO);

        ICreateContactCommandBuilderDTO AddPostalCode(string postalcode, ICreateContactCommandBuilderDTO createCommandDTO);

        ICreateContactCommandBuilderDTO AddTown(string town, ICreateContactCommandBuilderDTO createCommandDTO);

        ICreateContactCommandBuilderDTO AddPhone(string phone, ICreateContactCommandBuilderDTO createCommandDTO);

        ICreateContactCommandBuilderDTO AddEmail(string email, ICreateContactCommandBuilderDTO createCommandDTO);

        ICreateContactCommandDTO Build(string email, ICreateContactCommandBuilderDTO createCommandBuilder);
    }
}