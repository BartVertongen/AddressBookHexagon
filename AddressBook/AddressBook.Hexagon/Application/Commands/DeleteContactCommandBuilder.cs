//By Bart Vertongen copyright 2021

using System.IO;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Messaging;
using System;

namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class DeleteContactCommandBuilder : IDeleteContactCommandBuilder, IDeleteContactCommand
    {
        public ICommand Build()
        {
            DeleteContactCommand DeleteCommand;

            DeleteCommand = new DeleteContactCommand(Name);
            return DeleteCommand;
        }

        public string Name { get; private set; }

        public Guid Id { get; private set; }

        public IDeleteContactCommandBuilder AddName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidDataException("DeleteContactCommandBuilder::AddName: The 'name' argument can not be null or Empty!");
            else
            {
                this.Name = name;
                return this;
            }
        }
    }
}