//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.Messaging;
using System;

namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class UpdateContactCommand : ICommand
    {
        private readonly string _Name;
        private readonly string _NewStreet;
        private readonly string _NewPostalCode;
        private readonly string _NewTown;
        private readonly string _NewPhone;
        private readonly string _NewEmail;

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
        public UpdateContactCommand(string name,
                            string newStreet=null,
                            string newPostalCode=null,
                            string newTown=null,
                            string newPhone=null, 
                            string newEmail=null)
        {
            //TODO: validate Name, it cannot be empty
            _Name = name;
            _NewStreet = newStreet;
            _NewPostalCode = newPostalCode;
            _NewTown = newTown;
            _NewPhone = newPhone;
            _NewEmail = newEmail;
        }

        public string Name => _Name;

        public string Street => _NewStreet;

        public string PostalCode => _NewPostalCode;

        public string Town => _NewTown;

        public string Phone => _NewPhone;

        public string Email => _NewEmail;

        public Guid Id { get; private set; }
    }
}