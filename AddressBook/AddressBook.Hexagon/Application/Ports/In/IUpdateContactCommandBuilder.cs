//By Bart Vertongen


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IUpdateContactCommandBuilder
    {
        IUpdateContactCommandBuilder AddName(string name);

        IUpdateContactCommandBuilder AddStreet(string street);

        IUpdateContactCommandBuilder AddPostalCode(string postalcode);

        IUpdateContactCommandBuilder AddTown(string town);

        IUpdateContactCommandBuilder AddPhone(string phone);

        IUpdateContactCommandBuilder AddEmail(string email);

        IUpdateContactCommandDTO Build();

        string Name { get; }

        string Phone { get; }

        string Email { get; }

        string Street { get; }

        string PostalCode { get; }

        string Town { get; }
    }
}