//By Bart Vertongen copyright 2021

using System.IO;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class UpdateContactCommandBuilder : IUpdateContactCommandBuilder
    {
        public IUpdateContactCommandDTO Build()
        {
            IUpdateContactCommandDTO UpdateCommand = new UpdateContactCommandDTO();

            //TODO Validate before Building
            //this.ValidateObject();
            UpdateCommand.Name = this.Name;
            UpdateCommand.Phone = this.Phone;
            UpdateCommand.Email = this.Email;
            UpdateCommand.Street = this.Street;
            UpdateCommand.PostalCode = this.PostalCode;
            UpdateCommand.Town = this.Town;
            return UpdateCommand;
        }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string Street { get; private set; }

        public string PostalCode { get; private set; }

        public string Town { get; private set; }

        public IUpdateContactCommandBuilder AddName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidDataException("UpdateContactCommandBuilder::AddName: The 'name' argument can not be null or Empty!");
            else
            {
                this.Name = name;
                return this;
            }
        }

        public IUpdateContactCommandBuilder AddStreet(string street)
        {
            if (street == null)
                throw new InvalidDataException("UpdateContactBuilder::AddStreet: The 'street' argument can not be null!");
            else
            {
                this.Street = street;
                return this;
            }
        }

        public IUpdateContactCommandBuilder AddPostalCode(string postalcode)
        {
            if (postalcode == null)
                throw new InvalidDataException("UpdateContactBuilder::AddPostalCode: The 'postalcode' argument can not be null!");
            else
            {
                this.PostalCode = postalcode;
                return this;
            }
        }

        public IUpdateContactCommandBuilder AddTown(string town)
        {
            if (town == null)
                throw new InvalidDataException("UpdateContactCommandBuilder::AddTown: The 'town' argument can not be null!");
            else
            {
                this.Town = town;
                return this;
            }
        }

        public IUpdateContactCommandBuilder AddPhone(string phone)
        {
            if (phone == null)
                throw new InvalidDataException("UpdateContactCommandBuilder::AddPhone: The 'phone' argument can not be null!");
            else
            {
                Phone = phone;
                return this;
            }
        }

        public IUpdateContactCommandBuilder AddEmail(string email)
        {
            if (email == null)
                throw new InvalidDataException("UpdateContactCommandBuilder::AddEmail: The 'email' argument can not be null!");
            else
            {
                Email = email;
                return this;
            }
        }
    }
}