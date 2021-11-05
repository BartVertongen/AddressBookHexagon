//By Bart Vertongen

using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IUpdateContactCommandBuilder : ICommandBuilder, IUpdateContactCommandDTO
    {
        public IUpdateContactCommandBuilder AddName(string name);

        public IUpdateContactCommandBuilder AddStreet(string street);

        public IUpdateContactCommandBuilder AddPostalCode(string postalcode);

        public IUpdateContactCommandBuilder AddTown(string town);

        public IUpdateContactCommandBuilder AddPhone(string phone);

        public IUpdateContactCommandBuilder AddEmail(string email);
    }
}