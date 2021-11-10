//By Bart Vertongen copyright 2021

using System;
using PS.AddressBook.Hexagon.Application.Messaging;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class UpdateContactCommand : ICommand, IUpdateContactCommand
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
        /// <remarks>A UpdateContactCommand should be constructed using a Builder.</remarks>
        internal UpdateContactCommand(string name,
                            string street,
                            string postalCode,
                            string town,
                            string phone, string email)
        {
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