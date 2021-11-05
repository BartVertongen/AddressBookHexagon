//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application
{
    /// <summary>
    /// The CreateContactCommandBuilder Data Transfer Object
    /// </summary>
    public class CreateContactCommandBuilderDTO : ICreateContactCommandBuilderDTO
    {
        /// <summary>
        /// The default constructor for the Contact Data Transfer Object.
        /// </summary>
        public CreateContactCommandBuilderDTO()
        {
            Name = "";
            Phone = "";
            Email = "";
            Street = "";
            PostalCode = "";
            Town = "";
        }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }
    }
}