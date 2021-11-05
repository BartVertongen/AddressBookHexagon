//By Bart Vertongen copyright 2021

using System;
using PS.AddressBook.Hexagon.Application.Messaging;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class DeleteContactCommand : ICommand, IDeleteContactCommandDTO
    {
        public DeleteContactCommand(string name)
        {
            //TODO: validate Name, it cannot be empty
            //REM: Validation should be done by the DeleteContactCommandBuilder
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}