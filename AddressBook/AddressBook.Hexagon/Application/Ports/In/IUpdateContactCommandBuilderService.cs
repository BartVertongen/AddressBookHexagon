//By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IUpdateContactCommandBuilderService
    {
        IUpdateContactCommandBuilderDTO AddName(string name, IUpdateContactCommandBuilderDTO updateCommandBuilder);

        IUpdateContactCommandBuilderDTO AddStreet(string street, IUpdateContactCommandBuilderDTO updateCommandBuilder);

        IUpdateContactCommandBuilderDTO AddPostalCode(string postalcode, IUpdateContactCommandBuilderDTO updateCommandBuilder);

        IUpdateContactCommandBuilderDTO AddTown(string town, IUpdateContactCommandBuilderDTO updateCommandBuilder);

        IUpdateContactCommandBuilderDTO AddPhone(string phone, IUpdateContactCommandBuilderDTO updateCommandBuilder);

        IUpdateContactCommandBuilderDTO AddEmail(string email, IUpdateContactCommandBuilderDTO updateCommandBuilder);

        IUpdateContactCommandDTO Build(string email, IUpdateContactCommandBuilderDTO updateCommandBuilder);
    }
}