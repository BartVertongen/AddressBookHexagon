//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IDeleteContactCommandBuilder : ICommandBuilder
    {
        public string Name { get; }

        IDeleteContactCommandBuilder AddName(string name);
    }
}