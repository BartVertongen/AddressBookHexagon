//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.Messaging;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public interface ICommandBuilder
    {
        ICommand Build();
    }
}