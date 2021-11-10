//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface ICreateContactCommandBuilder : ICommandBuilder, ICreateContactCommand
    {
        ICreateContactCommandBuilder AddName(string name);

        ICreateContactCommandBuilder AddStreet(string street);

        ICreateContactCommandBuilder AddPostalCode(string postalcode);

        ICreateContactCommandBuilder AddTown(string town);

        ICreateContactCommandBuilder AddPhone(string phone);

        ICreateContactCommandBuilder AddEmail(string email);
    }
}