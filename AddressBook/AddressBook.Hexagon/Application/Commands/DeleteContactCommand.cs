//By Bart Vertongen copyright 2021

using System;
using PS.AddressBook.Hexagon.Application.Messaging;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class DeleteContactCommand : ICommand
    {
        private readonly string _Name;

        public DeleteContactCommand(string name)
        {
            //TODO: validate Name, it cannot be empty
            _Name = name;
        }

        public Guid Id { get; private set; }

        public string Name => _Name;
    }
}