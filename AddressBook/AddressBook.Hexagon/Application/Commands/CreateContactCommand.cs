//By Bart Vertongen copyright 2021

using System;
using PS.AddressBook.Hexagon.Application.Messaging;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class CreateContactCommand : ICommand, ICreateContactCommandDTO
    {
        /// <summary>
        /// Constructor of a CreateContactCommand
        /// </summary>
        /// <param name="name"></param>
        /// <param name="street"></param>
        /// <param name="postalCode"></param>
        /// <param name="town"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <remarks>We can do validation in the Command Constructor.</remarks>
        public CreateContactCommand(string name,
                            string street,
                            string postalCode,
                            string town,
                            string phone, string email)
        {
            //TODO: validate Name, it cannot be empty
            Name = name;
            Street = street;
            PostalCode = postalCode;
            Town = town;
            Phone = phone;
            Email = email;
        }

        public Guid Id { get; private set; }

        public string Name{ get; private set; }

        public string Street { get; private set; }

        public string PostalCode { get; private set; }

        public string Town { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }
    }
}