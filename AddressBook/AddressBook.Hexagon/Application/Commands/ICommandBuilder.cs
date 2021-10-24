
using PS.AddressBook.Hexagon.Application.Messaging;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public interface ICommandBuilder
    {
        ICommand Build();

        ICommandBuilder AddName(string name);

        ICommandBuilder AddPhone(string phone);

        ICommandBuilder AddEmail(string email);

        ICommandBuilder AddStreet(string street);

        ICommandBuilder AddPostalCode(string postalcode);

        ICommandBuilder AddTown(string town);
    }
}