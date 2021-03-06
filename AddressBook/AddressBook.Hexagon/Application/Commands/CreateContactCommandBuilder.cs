//By Bart Vertongen copyright 2021

using System.IO;
using PS.AddressBook.Hexagon.Application.Messaging;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class CreateContactCommandBuilder : ICreateContactCommandBuilder
    {
        public ICommand Build()
        {
            CreateContactCommand CreateCommand;

            //TODO Validate on highest level (complete object) before Building
            CreateCommand = new CreateContactCommand(Name, Street, PostalCode, Town, Phone, Email);
            return CreateCommand;
        }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string Street { get; private set; }

        public string PostalCode { get; private set; }

        public string Town { get; private set; }

        public ICreateContactCommandBuilder AddName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidDataException("CreateContactCommandBuilder::AddName: The 'name' argument can not be null or Empty!");
            else
            {
                this.Name = name;
                return this;
            }
        }

        public ICreateContactCommandBuilder AddStreet(string street)
        {
            if (street == null)
                throw new InvalidDataException("CreateContactBuilder::AddStreet: The 'street' argument can not be null!");
            else
            {
                this.Street = street;
                return this;
            }
        }

        public ICreateContactCommandBuilder AddPostalCode(string postalcode)
        {
            if (postalcode == null)
                throw new InvalidDataException("CreateContactBuilder::AddPostalCode: The 'postalcode' argument can not be null!");
            else
            {
                this.PostalCode = postalcode;
                return this;
            }
        }

        public ICreateContactCommandBuilder AddTown(string town)
        {
            if (town == null)
                throw new InvalidDataException("CreateContactCommandBuilder::AddTown: The 'town' argument can not be null!");
            else
            {
                this.Town = town;
                return this;
            }
        }

        public ICreateContactCommandBuilder AddPhone(string phone)
        {
            if (phone == null)
                throw new InvalidDataException("CreateContactCommandBuilder::AddPhone: The 'phone' argument can not be null!");
            else
            {
                Phone = phone;
                return this;
            }
        }

        public ICreateContactCommandBuilder AddEmail(string email)
        {
            if (email == null)
                throw new InvalidDataException("CreateContactCommandBuilder::AddEmail: The 'email' argument can not be null!");
            else
            {
                Email = email;
                return this;
            }
        }
    }
}