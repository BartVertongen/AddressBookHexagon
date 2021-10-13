//By Bart Vertongen copyright 2021

namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class CreateContactCommand
    {
        private readonly string _Name;
        private readonly string _Street;
        private readonly string _PostalCode;
        private readonly string _Town;
        private readonly string _Phone;
        private readonly string _Email;

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
            _Name = name;
            _Street = street;
            _PostalCode = postalCode;
            _Town = town;
            _Phone = phone;
            _Email = email;
        }

        public string Name => _Name;

        public string Street => _Street;

        public string PostalCode => _PostalCode;

        public string Town => _Town;

        public string Phone => _Phone;

        public string Email => _Email;
    }
}